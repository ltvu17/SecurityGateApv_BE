using AutoMapper;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Res;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.Notifications;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services
{
    public class ScheduleUserService : IScheduleUserService
    {
        private readonly IScheduleTypeRepo _scheduleTypeRepo;
        private readonly IScheduleUserRepo _scheduleUserRepo;
        private readonly IScheduleRepo _scheduleRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationRepo _notificationRepo;
        private readonly INotifications _notifications;
        private readonly IUserRepo _userRepo;
        private readonly IVisitRepo _visitRepo;



        public ScheduleUserService(IScheduleTypeRepo scheduleTypeRepo, IScheduleRepo scheduleRepo, IMapper mapper, IUnitOfWork unitOfWork, IScheduleUserRepo iScheduleUserRepo, INotificationRepo notificationRepo, INotifications notifications, IUserRepo userRepo, IVisitRepo visitRepo)
        {
            _scheduleTypeRepo = scheduleTypeRepo;
            _scheduleRepo = scheduleRepo;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _scheduleUserRepo = iScheduleUserRepo;
            _notificationRepo = notificationRepo;
            _notifications = notifications;
            _userRepo = userRepo;
            _visitRepo = visitRepo;
        }



        public async Task<Result<CreateScheduleUserCommand>> CreateScheduleUser(CreateScheduleUserCommand command)
        {
            var schedule = ((await _scheduleRepo.FindAsync(s => s.ScheduleId == command.ScheduleId))).FirstOrDefault();
            if (schedule.ScheduleTypeId == (int)ScheduleTypeEnum.VisitDaily)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.ScheduleCannotAssign);
            }
            if (schedule == null)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.NotFoundSchedule);
            }
            var scheduleUser = ScheduleUser.Create(
                command.Title,
                command.Description,
                command.Note,
                DateTime.Now,
                command.DeadlineTime,
                ScheduleUserStatusEnum.Assigned.ToString(),
                command.ScheduleId,
                command.AssignToId,
                command.MaxPersonQuantity,
                command.StartDate,
                command.EndDate
                );
            if (scheduleUser.IsFailure)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.ScheduleSaveError);
            }
            await _scheduleUserRepo.AddAsync(scheduleUser.Value);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.CommitError);
            }
            var noti = Notification.Create(command.Title, command.Description, scheduleUser.Value.Id.ToString(), DateTime.Now, null, (int)NotificationTypeEnum.ScheduleUser);
            noti.Value.AddUserNoti(schedule.CreateById, command.AssignToId);
            await _notificationRepo.AddAsync(noti.Value);
            var commit2 = await _unitOfWork.CommitAsync();
            if (!commit2)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.CommitError);
            }
            await _notifications.SendMessageAssignForStaff(command.Title, command.Description, command.AssignToId, command.ScheduleId);
            return command;
        }

        public Task<Result<ICollection<GetScheduleRes>>> GetAllSchedule(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<GetScheduleUserRes>>> GetScheduleUserByUserIdAndStatus(int userId, string status, int pageNumber, int pageSize)
        {
            var user = (await _userRepo.FindAsync(
                    s => s.UserId == userId,
                    includeProperties: "Role"
                )).FirstOrDefault();
            var scheduleUser = new List<ScheduleUser>();
            if (user.Role.RoleName == UserRoleEnum.Staff.ToString())
            {
                scheduleUser = (await _scheduleUserRepo.FindAsync(
                   s => s.AssignToId == userId
                   && (status == "All" || s.Status == status),
                   pageSize, pageNumber,
                   s => s.OrderByDescending(x => x.AssignTime),
                   includeProperties: "AssignTo,Schedule.ScheduleType,Schedule.CreateBy"
                   )).ToList();

            }
            else if (user.Role.RoleName == UserRoleEnum.DepartmentManager.ToString())
            {
                scheduleUser = (await _scheduleUserRepo.FindAsync(
                   s => s.Schedule.CreateById == userId
                   && (status == "All" || s.Status == status),
                   pageSize, pageNumber,
                   s => s.OrderByDescending(x => x.AssignTime),
                   includeProperties: "AssignTo,Schedule.ScheduleType,Schedule.CreateBy"
                   )).ToList();
            }
            else
            {
                scheduleUser = (await _scheduleUserRepo.FindAsync(
                  s => (status == "All" || s.Status == status),
                  pageSize, pageNumber,
                  s => s.OrderByDescending(x => x.AssignTime),
                  includeProperties: "AssignTo,Schedule.ScheduleType,Schedule.CreateBy"
                  )).ToList();
            }
            if (scheduleUser.Count() == 0)
            {
                return Result.Failure<List<GetScheduleUserRes>>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<List<GetScheduleUserRes>>(scheduleUser);
            return result;
        }
        public async Task<Result<ScheduleUserRes>> GetScheduleUserById(int scheduleUserId)
        {
            var scheduleUser = (await _scheduleUserRepo.FindAsync(
                     s => s.Id == scheduleUserId,
                     includeProperties: "Schedule.ScheduleType,Schedule.CreateBy,Visit"
                )).FirstOrDefault();
            if (scheduleUser == null)
            {
                return Result.Failure<ScheduleUserRes>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<ScheduleUserRes>(scheduleUser);
            return result;

        }
        public async Task<Result<List<GetScheduleUserRes>>> GetScheduleUserByUserId(int userId, int pageNumber, int pageSize)
        {
            var user = (await _userRepo.FindAsync(
                    s => s.UserId == userId,
                    includeProperties: "Role"
                )).FirstOrDefault();
            var scheduleUser = new List<ScheduleUser>();
            if (user.Role.RoleName == UserRoleEnum.Staff.ToString())
            {
                scheduleUser = (await _scheduleUserRepo.FindAsync(
                   s => s.AssignToId == userId,
                   pageSize, pageNumber,
                   s => s.OrderByDescending(x => x.AssignTime),
                   includeProperties: "AssignTo,Schedule.ScheduleType,Schedule.CreateBy"
                   )).ToList();

            }
            else if (user.Role.RoleName == UserRoleEnum.DepartmentManager.ToString())
            {
                scheduleUser = (await _scheduleUserRepo.FindAsync(
                   s => s.Schedule.CreateById == userId,
                   pageSize, pageNumber,
                   s => s.OrderByDescending(x => x.AssignTime),
                   includeProperties: "AssignTo,Schedule.ScheduleType,Schedule.CreateBy"
                   )).ToList();
            }
            if (scheduleUser == null)
            {
                return Result.Failure<List<GetScheduleUserRes>>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<List<GetScheduleUserRes>>(scheduleUser);
            return result;
        }

        public async Task<Result<bool>> RejectScheduleUser(int scheduleId)
        {
            var scheduleUser = await _scheduleUserRepo.GetByIdAsync(scheduleId);
            if (scheduleUser == null)
            {
                return Result.Failure<bool>(Error.NotFoundScheduleUser);
            }
            var visit = (await _visitRepo.FindAsync(
                s => s.ScheduleUserId == scheduleUser.Id)).FirstOrDefault();
            if (visit == null)
            {
                return Result.Failure<bool>(Error.ScheduleUserNotHaveVisit);
            }

            visit.UpdateStatus(VisitStatusEnum.Cancelled.ToString());
            await _visitRepo.UpdateAsync(visit);

            scheduleUser.UpdateStatus(ScheduleUserStatusEnum.Rejected.ToString());
            await _scheduleUserRepo.UpdateAsync(scheduleUser);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }
        public async Task<Result<bool>> AproveScheduleUser(int scheduleId)
        {
            var scheduleUser = await _scheduleUserRepo.GetByIdAsync(scheduleId);
            if (scheduleUser == null)
            {
                return Result.Failure<bool>(Error.NotFoundScheduleUser);
            }
            var visit = (await _visitRepo.FindAsync(
                s => s.ScheduleUserId == scheduleUser.Id)).FirstOrDefault();
            if (visit == null)
            {
                return Result.Failure<bool>(Error.ScheduleUserNotHaveVisit);
            }

            visit.UpdateStatus(VisitStatusEnum.Active.ToString());
            await _visitRepo.UpdateAsync(visit);

            scheduleUser.UpdateStatus(ScheduleUserStatusEnum.Approved.ToString());
            await _scheduleUserRepo.UpdateAsync(scheduleUser);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }

        public async Task<Result<int>> GetScheduleUserByStaffId(int staffId)
        {
            var scheduleUser = (await _scheduleUserRepo.FindAsync(s => s.AssignToId == staffId && s.Status == ScheduleUserStatusEnum.Assigned.ToString()));
            return scheduleUser.Count();
        }

        public async Task<Result<bool>> CancelScheduleUser(int scheduleId)
        {
            var scheduleUser = await _scheduleUserRepo.GetByIdAsync(scheduleId);
            if (scheduleUser == null)
            {
                return Result.Failure<bool>(Error.NotFoundScheduleUser);
            }
            var visit = (await _visitRepo.FindAsync(
                s => s.ScheduleUserId == scheduleUser.Id)).FirstOrDefault();
            if (visit != null)
            {
                return Result.Failure<bool>(Error.ScheduleUserHaveVisit);
            }

            scheduleUser.UpdateStatus(ScheduleUserStatusEnum.Cancel.ToString());
            await _scheduleUserRepo.UpdateAsync(scheduleUser);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<bool>(Error.CommitError);
            }
            return true;
        }
    }
}
