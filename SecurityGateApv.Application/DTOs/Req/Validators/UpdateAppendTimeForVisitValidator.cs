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
    public class UpdateAppendTimeForVisitValidator : AbstractValidator<UpdateAppendTimeForVisitCommand>
    {
        public UpdateAppendTimeForVisitValidator(IVisitorRepo visitorRepo)
        {
            RuleFor(s => s.UpdateById).Must(s =>
            {
                if (!visitorRepo.IsAny(t => t.VisitorId == s).GetAwaiter().GetResult())
                {
                    return false;
                }
                return true;
            });
        }
    }
}
