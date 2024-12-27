using AutoMapper;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.EmailSender;
using SecurityGateApv.Domain.Interfaces.Jwt;
using SecurityGateApv.Domain.Interfaces.Notifications;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using IEmailSender = SecurityGateApv.Domain.Interfaces.EmailSender.IEmailSender;

namespace SecurityGateApv.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IDepartmentRepo _departmentRepo;
        private readonly IRoleRepo _roleRepo;
        private readonly IMapper _mapper;
        private readonly IJwt _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly INotifications _notifications;
        private readonly IScheduleUserRepo _scheduleUserRepo;

        public UserService(IUserRepo userRepo, IMapper mapper, IJwt jwt, IDepartmentRepo departmentRepo,
            IRoleRepo roleRepo, IUnitOfWork unitOfWork, IEmailSender emailSender, INotifications notifications, IScheduleUserRepo scheduleUserRepo)
        {
            _userRepo = userRepo;
            _mapper = mapper;
            _jwt = jwt;
            _departmentRepo = departmentRepo;
            _roleRepo = roleRepo;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _notifications = notifications;
            _scheduleUserRepo = scheduleUserRepo;
        }

        public Task<Result<CreateUserComman>> CreateDepartmentManager(CreateUserComman command)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<CreateUserComman>> CreateUser(CreateUserComman command, string token)
        {
            var role = _jwt.DecodeJwt(token);
            var permission = await PermissionCheck(role, command.RoleID);
            if (!permission)
            {
                return Result.Failure<CreateUserComman>(Error.NotPermission);
            }
            var department = _departmentRepo.FindAsync(s => s.DepartmentId == command.DepartmentId).Result.FirstOrDefault();
            if ((command.RoleID.Equals(UserRoleEnum.Admin) && !department.DepartmentName.Equals(DepartmentSpecialCaseEnum.AdminDepartment.ToString()))
                || (command.RoleID.Equals(UserRoleEnum.Manager) && !department.DepartmentName.Equals(DepartmentSpecialCaseEnum.ManagerDepartment.ToString()))
                || (command.RoleID.Equals(UserRoleEnum.Security) && !department.DepartmentName.Equals(DepartmentSpecialCaseEnum.SecurityDepartment.ToString()))
                )
            {
                return Result.Failure<CreateUserComman>(Error.UserRoleNotMatchDepartment);
            }
            var userResult = User.Create(command.UserName, command.Password, command.FullName, command.Email, command.PhoneNumber, command.Image, DateTime.Now, DateTime.Now,
                    UserStatusEnum.Active.ToString(), command.RoleID, command.DepartmentId);
            if (userResult.IsFailure)
            {
                return Result.Failure<CreateUserComman>(userResult.Error);
            }
            await _userRepo.AddAsync(userResult.Value);
            await _unitOfWork.CommitAsync();
            await _emailSender.SendEmailAsync(userResult.Value.Email, "Welcome to APV security", "Xin chao a !");

            return command;
        }

        public async Task<Result<List<GetUserRes>>> GetAllUserPagingByDepartmentId(int pageNumber, int pageSize, int departmentId)
        {
            var department = (await _departmentRepo.FindAsync(
            s => s.DepartmentId == departmentId
                )).FirstOrDefault();
            if (department == null)
            {
                return Result.Failure<List<GetUserRes>>(Error.NotFoundDepartment);
            }
            var user = await _userRepo.FindAsync(
                    s => s.DepartmentId == department.DepartmentId,
                    pageSize, pageNumber, includeProperties: "Role"
                );
            if (user.Count() == 0)
            {
                return Result.Failure<List<GetUserRes>>(Error.NotFoundUser);
            }
            var result = _mapper.Map<List<GetUserRes>>(user);
            foreach(var staff in result)
            {
                staff.UserMission = (await _scheduleUserRepo.FindAsync(s => s.AssignToId == staff.UserId && s.Status == ScheduleUserStatusEnum.Assigned.ToString(), int.MaxValue)).Count();
            }
            return result;
        }

        public async Task<Result<List<GetUserRes>>> GetAllStaffPagingByDepartmentManagerId(int pageNumber, int pageSize, int departmentManagerId)
        {
            var departmentManager = (await _userRepo.FindAsync(
                s => s.UserId == departmentManagerId && s.Role.RoleName.Equals(UserRoleEnum.DepartmentManager.ToString())
                )).FirstOrDefault();
            if (departmentManager == null)
            {
                return Result.Failure<List<GetUserRes>>(Error.NotFoundDepartmentManager);
            }
            var user = await _userRepo.FindAsync(
                    s => s.DepartmentId == departmentManager.DepartmentId && s.Role.RoleName.Equals(UserRoleEnum.Staff.ToString()),
                    pageSize, pageNumber, includeProperties: "Role,Department"
                );
            if (user.Count() == 0)
            {
                return Result.Failure<List<GetUserRes>>(Error.NotFoundUser);
            }
            var result = _mapper.Map<List<GetUserRes>>(user);
            return result;
        }

        public async Task<Result<List<GetUserRes>>> GetUserByRolePaging(int pageNumber, int pageSize, string role)
        {
            var user = new List<User>();
            if (role == "All")
            {
                user = (await _userRepo.FindAsync(
                    s => true,
                    pageSize, pageNumber, includeProperties: "Role,Department"
                    )).ToList();
            }
            else
            {
                user = (await _userRepo.FindAsync(
                    s => s.Role.RoleName.Equals(role),
                    pageSize, pageNumber, includeProperties: "Role,Department"
                    )).ToList();
            }

            if (user.Count() == 0)
            {
                return Result.Failure<List<GetUserRes>>(Error.NotFoundUser);
            }
            var result = _mapper.Map<List<GetUserRes>>(user);
            return result;
        }

        public async Task<Result<LoginRes>> Login(LoginModel loginModel)
        {
            var users = await _userRepo.FindAsync(
                             s => s.UserName == loginModel.Username &&
                                  s.Status == UserStatusEnum.Active.ToString(),
                             includeProperties: "Role,Department"
                         );

            var login = users.FirstOrDefault();

            if (login == null)
            {
                return Result.Failure<LoginRes>(Error.NotFoundUserLogin);
            }

            if (!string.Equals(login.Password, loginModel.Password, StringComparison.Ordinal))
            {
                return Result.Failure<LoginRes>(Error.IncorrectPassword);
            }

            var result = new LoginRes
            {
                UserId = login.UserId,
                UserName = login.UserName,
                JwtToken = _jwt.GenerateJwtToken(login)
            };

            return Result.Success(result);
        }

        public async Task<Result<bool>> SendEmailTest(string email)
        {
            await _emailSender.SendEmailAsync("luutranvu17@gmail.com", "APV", "hello");
            return true;
        }

        public async Task<Result<bool>> UnactiveUser(int userId, string token)
        {

            var user = (await _userRepo.FindAsync(s => s.UserId == userId)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<bool>(Error.NotFoundUser);
            }
            var role = _jwt.DecodeJwt(token);
            var permission = await PermissionCheck(role, user.RoleId);
            if (!permission)
            {
                return Result.Failure<bool>(Error.NotPermission);
            }
            user.Unactive();
            await _userRepo.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return true;
        }

        public async Task<Result<UpdateUserCommand>> UpdateUser(int userId, UpdateUserCommand command, string token)
        {
            // Decode the JWT token to get the role of the user making the request
            var role = _jwt.DecodeJwt(token);

            // Check if the user has permission to update the specified role
            var permission = await PermissionCheck(role, command.RoleID);
            if (!permission)
            {
                return Result.Failure<UpdateUserCommand>(Error.NotPermission);
            }

            // Retrieve the user by userId from the repository
            var user = (await _userRepo.FindAsync(s => s.UserId == userId)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<UpdateUserCommand>(Error.NotFoundUser);
            }

            // Check if the username in the command matches the existing username of the user
            if (user.UserName != command.UserName)
            {
                return Result.Failure<UpdateUserCommand>(Error.CanNotUpdateUserName);
            }

            var department = _departmentRepo.FindAsync(s => s.DepartmentId == command.DepartmentId).Result.FirstOrDefault();
            // If the user's role is not Admin, Manager, or Security, check the department
            if (user.RoleId == (int)UserRoleEnum.Staff ||
                user.RoleId == (int)UserRoleEnum.DepartmentManager)
            {
                // Check if the specified DepartmentId in the command exists in the repository
                if (department == null
                    || department.DepartmentName.Equals(DepartmentSpecialCaseEnum.AdminDepartment.ToString())
                    || department.DepartmentName.Equals(DepartmentSpecialCaseEnum.ManagerDepartment.ToString())
                    || department.DepartmentName.Equals(DepartmentSpecialCaseEnum.SecurityDepartment.ToString()))
                {
                    return Result.Failure<UpdateUserCommand>(Error.NotFoundDepartment);
                }
            }
            if (user.RoleId == (int)UserRoleEnum.Admin
                || user.RoleId == (int)UserRoleEnum.Manager
                || user.RoleId == (int)UserRoleEnum.Security)
            {
                if (command.DepartmentId != null && command.DepartmentId != user.DepartmentId)
                {
                    return Result.Failure<UpdateUserCommand>(Error.CanNotUpdateDepartment);
                }
            }


            // Map the properties from the command to the user object
            var result = _mapper.Map(command, user);

            // Call the Update method on the user object to apply any additional logic required for updating
            user.Update();

            // Update the user in the repository and commit the changes to the database
            if (!await _userRepo.UpdateAsync(result))
            {
                return Result.Failure<UpdateUserCommand>(Error.UpdateDepartment);
            }
            try
            {
                if (!await _unitOfWork.CommitAsync())
                {
                    return Result.Failure<UpdateUserCommand>(Error.UpdateDepartment);
                }
            }
            catch (Exception ex)
            {
                // Log exception details here
                Console.WriteLine($"Error committing changes: {ex.Message}");
                return Result.Failure<UpdateUserCommand>(Error.UpdateDepartment);
            }

            return command;
        }
        private async Task<bool> PermissionCheck(string userRole, int checkRole)
        {
            //check if userRole is Admin, Manager, DepartmentManager, Staff, or Security and checkRole is Admin, Manager, DepartmentManager, Staff, or Security respectively true
            if (((UserRoleEnum)checkRole).ToString() == userRole)
            {
                return true;
            }
            if (userRole == UserRoleEnum.Manager.ToString() && (checkRole == (int)UserRoleEnum.DepartmentManager || checkRole == (int)UserRoleEnum.Staff || checkRole == (int)UserRoleEnum.Security))
            {
                return true;
            }
            else
            if (userRole == UserRoleEnum.DepartmentManager.ToString() && checkRole == (int)UserRoleEnum.Staff)
            {
                return true;
            }
            else
            if (userRole == UserRoleEnum.Admin.ToString())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<Result<GetUserRes>> GetUserById(int userId)
        {
            var user = (await _userRepo.FindAsync(s => s.UserId == userId, includeProperties: "Role,Department")).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<GetUserRes>(Error.NotFoundUser);
            }
            return _mapper.Map<GetUserRes>(user);
        }
        public async Task<Result<GetUserRes>> GetStaffByPhone(string phonnumber)
        {
            var user = (await _userRepo.FindAsync(s => s.PhoneNumber == phonnumber, includeProperties: "Role,Department")).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<GetUserRes>(Error.NotFoundUser);
            }
            return _mapper.Map<GetUserRes>(user);
        }

        public async Task<Result<bool>> UpdateUserPassword(int userId, UpdateUserPasswordCommand command)
        {
            var user = (await _userRepo.FindAsync(s => s.UserId == userId)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<bool>(Error.NotFoundUser);
            }
            if (user.Password != command.OldPassword)
            {
                return Result.Failure<bool>(Error.PasswordNotMatch);
            }
            if (command.NewPassword != command.NewPasswordCheck)
            {
                return Result.Failure<bool>(Error.CheckPasswordError);
            }
            user.UpdatePassword(command.NewPassword);
            await _userRepo.UpdateAsync(user);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }

        public async Task<Result<UpdateUserNoDepartmentIdCommand>> UpdateUserNodepartmentId(int userId, UpdateUserNoDepartmentIdCommand command, string token)
        {
            var user = (await _userRepo.FindAsync(s => s.UserId == userId)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<UpdateUserNoDepartmentIdCommand>(Error.NotFoundUser);
            }
            if (user.UserName != command.UserName)
            {
                return Result.Failure<UpdateUserNoDepartmentIdCommand>(Error.CanNotUpdateUserName);
            }
            user = _mapper.Map(command, user);
            user.Update();
            await _userRepo.UpdateAsync(user);
            await _unitOfWork.CommitAsync();
            return command;
        }

        public async Task<Result<bool>> SendOTPResetPassword(string email)
        {
            var hasUser = await _userRepo.IsAny(s => s.Email == email);
            if (!hasUser)
            {
                return Result.Failure<bool>(Error.EmailResetPasswordNotValid);
            }
            var OTP = "";
            for(var i = 0; i < 6; i++)
            {
                Random rnd = new Random();
                OTP += rnd.Next(0, 9);
            }
            var user = (await _userRepo.FindAsync(s => s.Email == email)).FirstOrDefault();
            if (user == null)
            {
                return Result.Failure<bool>(Error.EmailResetPasswordNotValid);
            }
            user.SetOTP(OTP);
            await _userRepo.UpdateAsync(user);
            await _emailSender.SendEmailAsync(email, "OTP cấp lại mật khẩu", OTP);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }

        public async Task<Result<bool>> ConfirmOTPResetPassword(string email, string OTP)
        {
            var hasUser = await _userRepo.IsAny(s => s.Email == email);
            if (!hasUser)
            {
                return Result.Failure<bool>(Error.EmailResetPasswordNotValid);
            }
            var user = (await _userRepo.FindAsync(s => s.Email == email)).FirstOrDefault();
            if (user == null || user.OTP == null)
            {
                return Result.Failure<bool>(Error.EmailResetPasswordNotValid);
            }
            if(user.OTP != OTP)
            {
                return Result.Failure<bool>(Error.OTPNotEqual);
            }
            if((DateTime.Now - user.OTPIssueTime) > TimeSpan.FromMinutes(1))
            {
                return Result.Failure<bool>(Error.OTPExpired);
            }
            user.SetOTP(OTP);
            await _userRepo.UpdateAsync(user);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }

        public async Task<Result<bool>> SetNewPassword(string email, string OTP, string newPassword)
        {
            var hasUser = await _userRepo.IsAny(s => s.Email == email);
            if (!hasUser)
            {
                return Result.Failure<bool>(Error.EmailResetPasswordNotValid);
            }
            var user = (await _userRepo.FindAsync(s => s.Email == email)).FirstOrDefault();
            if (user == null || user.OTP == null)
            {
                return Result.Failure<bool>(Error.EmailResetPasswordNotValid);
            }
            if (user.OTP != OTP)
            {
                return Result.Failure<bool>(Error.OTPNotEqual);
            }
            if ((DateTime.Now - user.OTPIssueTime) > TimeSpan.FromMinutes(2))
            {
                return Result.Failure<bool>(Error.OTPExpired);
            }
            user.SetNewPassword(newPassword);
            await _userRepo.UpdateAsync(user);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }
    }
}
