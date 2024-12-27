using AutoMapper;
using Azure.Core;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
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
    public class ScheduleService : IScheduleService
    {
        private readonly IScheduleRepo _scheduleRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScheduleTypeRepo _scheduleTypeRepo;
        private readonly IScheduleUserRepo _scheduleUserRepo;
        private readonly INotifications _notifications;
        private readonly INotificationRepo _notificationRepo;
        private readonly IMapper _mapper;

        public ScheduleService(IScheduleRepo scheduleRepo, IUnitOfWork unitOfWork, IMapper mapper, 
            IScheduleTypeRepo scheduleTypeRepo, IScheduleUserRepo scheduleUserRepo, INotifications notifications, INotificationRepo notificationRepo)
        {
            _scheduleRepo = scheduleRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _scheduleTypeRepo = scheduleTypeRepo;
            _scheduleUserRepo = scheduleUserRepo;
            _notifications = notifications;
            _notificationRepo = notificationRepo;
        }

        public async Task<Result<CreateScheduleCommand>> CreateSchedule(CreateScheduleCommand request)
        {
            var scheduleCreate = Schedule.Create(
                request.ScheduleName,
                request.DaysOfSchedule,
                request.Description,
                DateTime.Now,
                DateTime.Now,
                true,
                request.ScheduleTypeId,
                request.CreateById
               );
            if (scheduleCreate.IsFailure)
            {
                return Result.Failure<CreateScheduleCommand>(Error.ScheduleCreateError);
            }
            await _scheduleRepo.AddAsync(scheduleCreate.Value);
            await _unitOfWork.CommitAsync();

            return request;
        }

        public async Task<Result<bool>> DeleteSchedule(int scheduleId)
        {
            var schedule = await _scheduleRepo.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                return Result.Failure<bool>(Error.NotFoundSchedule);
            }
            schedule.UpdateStatus(false);
            if (!await _scheduleRepo.UpdateAsync(schedule))
            {
                return Result.Failure<bool>(Error.ScheduleCreateError);
            }
            if (!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<bool>(Error.ScheduleSaveError);
            }
            return true;
        }
        public async Task<Result<ICollection<GetScheduleRes>>> GetAllSchedule(int pageNumber, int pageSize)
        {
            IEnumerable<Schedule> schedule;
            if (pageNumber == -1 || pageSize == -1)
            {
                schedule = await _scheduleRepo.FindAsync(
                s => true, int.MaxValue, 1, 
                s => s.OrderByDescending(x => x.CreateTime),
                includeProperties: "ScheduleType,CreateBy"
                );
            }
            else
            {
                schedule = await _scheduleRepo.FindAsync(
                s => true, pageSize, pageNumber, includeProperties: "ScheduleType,CreateBy"
                );
            }
            if (schedule == null || !schedule.Any())
            {
                return  Result.Failure<ICollection<GetScheduleRes>>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<IEnumerable<GetScheduleRes>>(schedule);
            return result.ToList();
        }


        public async Task<Result<GetScheduleRes>> GetScheduleById(int scheduleId)
        {
            var schedule = (await _scheduleRepo.FindAsync(
                s => s.ScheduleId == scheduleId ,includeProperties: "ScheduleType,CreateBy"
                )).FirstOrDefault();
            if (schedule == null)
            {
                return Result.Failure<GetScheduleRes>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<GetScheduleRes>(schedule);
            return result;
        }

        public async Task<Result<GetScheduleRes>> UpdateSchedule(UpdateScheduleCommand request, int scheduleId)
        {
            var userschedule = (await _scheduleUserRepo.IsAny(s => s.ScheduleId == scheduleId && s.Status == ScheduleUserStatusEnum.Approved.ToString()));
            if (userschedule)
            {
                return Result.Failure<GetScheduleRes>(Error.CanNotUpdateSchedule);
            }
            var schedule = await _scheduleRepo.GetByIdAsync(scheduleId);
            if (schedule == null)
            {
                return Result.Failure<GetScheduleRes>(Error.NotFoundSchedule);
            }
            if(schedule.ScheduleName.Equals(ScheduleTypeEnum.VisitDaily.ToString()))
            {
                return Result.Failure<GetScheduleRes>(Error.ScheduleCannotUpdate);
            }
            request.CreateById = schedule.CreateById;
            request.ScheduleTypeId = schedule.ScheduleTypeId;
            if(! await ValidateDaysOfProcess(request))
            {
                return Result.Failure<GetScheduleRes>(Error.ScheduleValid);
            }
            schedule.Update(request.ScheduleName, request.DaysOfSchedule, request.Description, schedule.CreateTime, DateTime.Now, request.Status, request.ScheduleTypeId, request.CreateById);
            if(!await _scheduleRepo.UpdateAsync(schedule))
            {
                return Result.Failure<GetScheduleRes> (Error.ScheduleCreateError);
            }
            if (!await _unitOfWork.CommitAsync())
            {
                return Result.Failure<GetScheduleRes>(Error.ScheduleSaveError);
            }
            var result = _mapper.Map<GetScheduleRes>(schedule);
            return result;
        }
        private async Task<bool> ValidateDaysOfProcess(UpdateScheduleCommand command)
        {
            var scheduleType = await _scheduleTypeRepo.GetByIdAsync(command.ScheduleTypeId);
            if (scheduleType == null) return false;


            if (scheduleType.ScheduleTypeName.Equals(ScheduleTypeEnum.ProcessWeek.ToString()))
            {

                return IsValidDaysOfProcess(command.DaysOfSchedule, 1, 7);
            }

            if (scheduleType.ScheduleTypeName.Equals(ScheduleTypeEnum.ProcessMonth.ToString()))
            {
                return IsValidDaysOfProcess(command.DaysOfSchedule, 1, 31);
            }
            var days = command.DaysOfSchedule.Split(',')
                            .Select(d => d.Trim());
            if (days.Any())
            {
                return days.All(day =>
                        int.TryParse(day, out int result) &&
                        result >= 1 && result <= 31
                    );
            }

            return true;
        }
        private bool IsValidDaysOfProcess(string daysOfProcess, int min, int max)
        {
            var days = daysOfProcess.Split(',').Select(d => d.Trim());
            return days.All(day => int.TryParse(day, out int result) && result >= min && result <= max);
        }

        public async Task<Result<CreateScheduleUserCommand>> CreateScheduleUser(CreateScheduleUserCommand command)
        {
            var schedule = ((await _scheduleRepo.FindAsync(s => s.ScheduleId == command.ScheduleId))).FirstOrDefault();
            if (schedule.ScheduleTypeId == (int)ScheduleTypeEnum.VisitDaily)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.ScheduleCannotAssign);
            }
            var scheduleUser = ScheduleUser.Create(
                command.Title,
                command.Description,
                command.Note,
                DateTime.Now,
                command.DeadlineTime,
                "Assigned",
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
            var noti = Notification.Create(command.Title, command.Description, scheduleUser.Value.Id.ToString(), DateTime.Now, null, (int)NotificationTypeEnum.ScheduleUser);
            noti.Value.AddUserNoti(schedule.CreateById, command.AssignToId);
            await _notificationRepo.AddAsync(noti.Value);
            var commit = await _unitOfWork.CommitAsync();
            if (!commit)
            {
                return Result.Failure<CreateScheduleUserCommand>(Error.CommitError);
            }
            await _notifications.SendMessageAssignForStaff(command.Title, command.Description, command.AssignToId, command.ScheduleId);
            return command;
        }

        public async Task<Result<List<GetScheduleRes>>> GetScheduleByDepartmentManagerId(int departmentManagerId, int pageNumber, int pageSize)
        {
            var schedule = (await _scheduleRepo.FindAsync(
                s => s.CreateById == departmentManagerId,pageSize,pageNumber, s => s.OrderByDescending(x => x.CreateTime) ,includeProperties: "ScheduleType,CreateBy,ScheduleUser"
                ));
            if (schedule == null)
            {
                return Result.Failure<List<GetScheduleRes>>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<List<GetScheduleRes>>(schedule);
            return result;
        }

        public async Task<Result<List<GetScheduleUserRes>>> GetScheduleUserByStaffId(int staffId, int pageNumber, int pageSize)
        {
            var schedule = (await _scheduleUserRepo.FindAsync(
                s => s.AssignToId == staffId, pageSize, pageNumber, includeProperties: "Schedule,AssignTo,AssignFrom,Schedule.ScheduleType"
                ));
            if (schedule == null)
            {
                return Result.Failure<List<GetScheduleUserRes>>(Error.NotFoundSchedule);
            }
            var result = _mapper.Map<List<GetScheduleUserRes>>(schedule);
            return result;
        }

        public async Task<Result<int>> GetScheduleNotReadByStaffId(int staffId)
        {
            var schedule = (await _scheduleUserRepo.FindAsync(s => s.AssignToId == staffId && s.Status == ScheduleUserStatusEnum.Pending.ToString(),int.MaxValue));
            return schedule.Count();
        }
    }
}
