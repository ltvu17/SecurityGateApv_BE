using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface IUserService
    {
        public Task<Result<LoginRes>> Login(LoginModel loginModel);
        public Task<Result<List<GetUserRes>>> GetUserByRolePaging(int pageNumber, int pageSize, string role);
        public Task<Result<GetUserRes>> GetUserById(int userId);
        public Task<Result<GetUserRes>> GetStaffByPhone(string  phonnumber);
        public Task<Result<List<GetUserRes>>> GetAllUserPagingByDepartmentId(int pageNumber, int pageSize, int  departmentId);
        public Task<Result<List<GetUserRes>>> GetAllStaffPagingByDepartmentManagerId(int pageNumber, int pageSize, int  departmentManagerId);
        public Task<Result<CreateUserComman>> CreateUser(CreateUserComman command, string token);
        public Task<Result<UpdateUserCommand>> UpdateUser(int userId, UpdateUserCommand command, string token);
        public Task<Result<UpdateUserNoDepartmentIdCommand>> UpdateUserNodepartmentId(int userId, UpdateUserNoDepartmentIdCommand command, string token);
        public Task<Result<bool>> UpdateUserPassword(int userId, UpdateUserPasswordCommand command);
        public Task<Result<bool>> UnactiveUser(int userId, string token);
        public Task<Result<bool>> SendEmailTest(string email);
        public Task<Result<bool>> SendOTPResetPassword(string email);
        public Task<Result<bool>> ConfirmOTPResetPassword(string email, string OTP);
        public Task<Result<bool>> SetNewPassword(string email, string OTP, string newPassword);
    }
}
