using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.Validators
{
    public class DepartmentCreateValidator : AbstractValidator<DepartmentCreateCommand>
    {
        public DepartmentCreateValidator()
        {
            RuleFor(s=> s.DepartmentName).NotEmpty().NotNull();
            RuleFor(s=> s.Description).NotEmpty().NotNull();
            RuleFor(s=> s.AcceptLevel).NotNull().Must(s=>s>0);
        }
    }
}
