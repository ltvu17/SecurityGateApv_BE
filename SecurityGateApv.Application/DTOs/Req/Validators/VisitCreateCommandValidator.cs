using FluentValidation;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.Validators
{
    public class VisitCreateCommandValidator : AbstractValidator<VisitCreateCommand>
    {
        public VisitCreateCommandValidator(IUserRepo userRepo, IVisitorRepo visitorRepo, IScheduleUserRepo scheduleRepo)
        {
            RuleFor(s => s.VisitName).NotNull().NotEmpty();
            RuleFor(s => s.ExpectedEndTime).NotNull().NotEmpty();
            RuleFor(s => s.ExpectedStartTime).NotNull().NotEmpty();
            RuleFor(s => s.Description).NotNull().NotEmpty();
            RuleFor(s => s.VisitQuantity).NotNull().NotEmpty().Must(s =>s>0).WithMessage("Số lượng phải lớn hơn 0");
            RuleFor(s => new {s.VisitQuantity, s.VisitDetail}).NotNull().NotEmpty().Must(s =>
            {
                if(s.VisitQuantity == s.VisitDetail.Count)
                {
                    return true;
                }
                return false;
            }).WithMessage("Số lượng khác với chi tiết");
            RuleFor(s => s.CreateById).NotNull().NotEmpty().Must(s =>
            {
                return userRepo.IsAny(t=> t.UserId == s).GetAwaiter().GetResult();
            }).WithMessage("Người tạo này không tồn tại");
            RuleFor(s => s.ResponsiblePersonId).NotNull().NotEmpty().Must(s =>
            {
                return userRepo.IsAny(t=> t.UserId == s).GetAwaiter().GetResult();
            }).WithMessage("Người chịu trách nhiệm không tồn tại");
            RuleFor(s => s.ScheduleUserId).NotNull().NotEmpty().Must(s =>
            {
                return scheduleRepo.IsAny(t => t.Id == s).GetAwaiter().GetResult();
            }).WithMessage("Không tìm thấy nhiệm vụ này");
            RuleForEach(s => s.VisitDetail).NotNull().NotEmpty().Must(x =>
            {
                if (x.ExpectedEndHour < x.ExpectedStartHour)
                {
                    return false;
                }
                //if(x.ExpectedEndHour > TimeSpan.Parse("20:00:00"))
                //{
                //    return false;
                //}
                //if (x.ExpectedStartHour < TimeSpan.Parse("07:00:00"))
                //{
                //    return false;
                //}
                if (!visitorRepo.IsAny(s=>s.VisitorId == x.VisitorId).GetAwaiter().GetResult())
                {
                    return false;
                }
                return true;
            }
            ).WithMessage("Thời gian không hợp lệ hoặc không tìm thấy khách này trong hệ thống");
        }

            
    }
    public class VisitCreateCommandDailyValidator : AbstractValidator<VisitCreateCommandDaily>
    {
        public VisitCreateCommandDailyValidator(IUserRepo userRepo, IVisitorRepo visitorRepo)
        {
            RuleFor(s => s.VisitName).NotNull().NotEmpty();
            RuleFor(s => s.ExpectedEndTime).NotNull().NotEmpty();
            RuleFor(s => s.ExpectedStartTime).NotNull().NotEmpty();
            RuleFor(s => s.Description).NotNull().NotEmpty();
            RuleFor(s => s.VisitQuantity).NotNull().NotEmpty().Must(s => s > 0).WithMessage("Số lượng phải lớn hơn 0");
            RuleFor(s => new { s.VisitQuantity, s.VisitDetail }).NotNull().NotEmpty().Must(s =>
            {
                if (s.VisitQuantity == s.VisitDetail.Count)
                {
                    return true;
                }
                return false;
            }).WithMessage("Số lượng khác với chi tiết");
            RuleFor(s => s.CreateById).NotNull().NotEmpty().Must(s =>
            {
                return userRepo.IsAny(t => t.UserId == s).GetAwaiter().GetResult();
            }).WithMessage("Người tạo này không tồn tại");
            RuleFor(s => s.ResponsiblePersonId).NotNull().NotEmpty().Must(s =>
            {
                return userRepo.IsAny(t => t.UserId == s).GetAwaiter().GetResult();
            }).WithMessage("Người chịu trách nhiệm không tồn tại");
            RuleForEach(s => s.VisitDetail).NotNull().NotEmpty().Must(x =>
            {
                if (x.ExpectedEndHour < x.ExpectedStartHour)
                {
                    return false;
                }
                //if (x.ExpectedEndHour > TimeSpan.Parse("20:00:00"))
                //{
                //    return false;
                //}
                //if (x.ExpectedStartHour < TimeSpan.Parse("07:00:00"))
                //{
                //    return false;
                //}
                if (!visitorRepo.IsAny(s => s.VisitorId == x.VisitorId).GetAwaiter().GetResult())
                {
                    return false;
                }
                return true;
            }
            ).WithMessage("Thời gian không hợp lệ hoặc không tìm thấy khách này trong hệ thống");
        }


    }
}
