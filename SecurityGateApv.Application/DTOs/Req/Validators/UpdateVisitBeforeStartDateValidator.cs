using FluentValidation;
using SecurityGateApv.Application.DTOs.Req.UpdateReq;
using SecurityGateApv.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.Validators
{
    public class UpdateVisitBeforeStartDateValidator : AbstractValidator<UpdateVisitBeforeStartDateCommand>
    {
        public UpdateVisitBeforeStartDateValidator(IVisitorRepo visitorRepo)
        {
            RuleFor(s => s.VisitName).NotNull().NotEmpty();
            RuleFor(s => s.ExpectedEndTime).NotNull().NotEmpty();
            RuleFor(s => s.ExpectedStartTime).NotNull().NotEmpty();
            RuleFor(s => s.Description).NotNull().NotEmpty();
            RuleFor(s => s.VisitQuantity).NotNull().NotEmpty().Must(s => s > 0).WithMessage("Must greater than zero");
            RuleFor(s => new { s.VisitQuantity, s.VisitDetail }).NotNull().NotEmpty().Must(s =>
            {
                if (s.VisitQuantity == s.VisitDetail.Count)
                {
                    return true;
                }
                return false;
            }).WithMessage("Quantity not match visit detail");
            RuleForEach(s => s.VisitDetail).NotNull().NotEmpty().Must(x =>
            {
                if (x.ExpectedEndHour <= x.ExpectedStartHour)
                {
                    return false;
                }
                if (!visitorRepo.IsAny(s => s.VisitorId == x.VisitorId).GetAwaiter().GetResult())
                {
                    return false;
                }
                return true;
            }
            ).WithMessage("Time, visitor id not valid");
        }
    }
}
