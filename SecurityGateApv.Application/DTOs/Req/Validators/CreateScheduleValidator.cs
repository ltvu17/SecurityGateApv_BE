using FluentValidation;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.Validators
{

    public class CreateScheduleValidator : AbstractValidator<CreateScheduleCommand>
    {
        private readonly IScheduleTypeRepo _scheduleTypeRepo;
        private readonly IUserRepo _userRepo;

        public CreateScheduleValidator(IScheduleTypeRepo scheduleTypeRepo, IUserRepo userRepo)
        {
            _scheduleTypeRepo = scheduleTypeRepo;
            _userRepo = userRepo;

            RuleFor(s => s.ScheduleName)
            .NotEmpty()
            .WithMessage("Yêu cầu nhập tên lịch trình.");

            RuleFor(s => s.DaysOfSchedule)
                .NotEmpty()
                .WithMessage("Yêu cần chọn ngày cho lịch trình.");


            RuleFor(s => s.Description)
                .NotEmpty()
                .WithMessage("Yêu cầu nhập mô tả.");

            RuleFor(s => s.ScheduleTypeId)
                .NotEmpty()
                .WithMessage("Yêu cầu chọn loại lịch trình.")
                .Must( (id, cancellation) => _scheduleTypeRepo.IsAny(x => x.ScheduleTypeId == id.ScheduleTypeId).GetAwaiter().GetResult()
                ).WithMessage("Loại lịch trình không tồn tại.");

            RuleFor(s => s.CreateById)
                .NotEmpty()
                .WithMessage("Yêu cậu nhập người tạo.")
                .Must( (id, cancellation) =>
                {
                    return  _userRepo.IsAny(x => x.UserId == id.CreateById).GetAwaiter().GetResult();
                }).WithMessage("Người tạo ko tồn tại.");

             RuleFor(s => s)
                .Must((command) => ValidateDaysOfProcess(command).GetAwaiter().GetResult())
                .WithMessage("Ngày của lịch trình được chọn không phù hợp với loại lịch trình.");
        }
        private async Task<bool> ValidateDaysOfProcess(CreateScheduleCommand command)
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

        private bool AreValidDaysForProject(string daysOfProcess, int month)
        {
            var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, month);
            var days = daysOfProcess.Split(',').Select(d => d.Trim());

            return days.All(day =>
            {
                if (int.TryParse(day, out int result))
                {
                    return result > 0 && result <= daysInMonth;
                }
                return false;
            });
        }
    }
}
