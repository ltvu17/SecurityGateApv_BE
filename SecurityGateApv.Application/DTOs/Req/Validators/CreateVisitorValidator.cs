using FluentValidation;
using SecurityGateApv.Application.DTOs.Req.CreateReq;
using SecurityGateApv.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.Validators
{
    public class CreateVisitorValidator : AbstractValidator<CreateVisitorCommand>
    {
        public CreateVisitorValidator(ICredentialCardTypeRepo credentialCardTypeRepo, IVisitorRepo visitorRepo)
        {
            RuleFor(s =>s.VisitorName).NotNull().WithMessage("Cần nhập tên khách").NotEmpty().WithMessage("Cần nhập tên khách")
                .Matches(@"^[A-Za-zàáãạảăắằẳẵặâấầẩẫậèéẹẻẽêềếểễệđìíĩỉịòóõọỏôốồổỗộơớờởỡợùúũụủưứừửữựỳỵỷỹýÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬÈÉẸẺẼÊỀẾỂỄỆĐÌÍĨỈỊÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢÙÚŨỤỦƯỨỪỬỮỰỲỴỶỸÝ]+(?:[-'\s.][A-Za-zàáãạảăắằẳẵặâấầẩẫậèéẹẻẽêềếểễệđìíĩỉịòóõọỏôốồổỗộơớờởỡợùúũụủưứừửữựỳỵỷỹýÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬÈÉẸẺẼÊỀẾỂỄỆĐÌÍĨỈỊÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢÙÚŨỤỦƯỨỪỬỮỰỲỴỶỸÝ]+)+$").WithMessage("Tên người dùng chỉ bao gồm chữ và 2 từ trở lên");
            RuleFor(s => s.CompanyName).NotNull().NotEmpty();
            RuleFor(s => s.CredentialsCard).NotNull().NotEmpty().Must(s =>
            {
                if(visitorRepo.IsAny(t => t.CredentialsCard == s).GetAwaiter().GetResult())
                {
                    return false;
                }
                return true;
            }).WithMessage("Số thẻ này đã được đăng kí trước đó");
            RuleFor(s => s.PhoneNumber).NotNull().NotEmpty()
                           .Matches(@"^(0\d{9})$").WithMessage("Số điện thoại phải đúng 10 số và bắt đầu bằng số 0"); 
            RuleFor(s => s.CredentialCardTypeId).NotNull().NotEmpty().Must(s => {
                return credentialCardTypeRepo.IsAny(t => t.CredentialCardTypeId == s).GetAwaiter().GetResult();
            }).WithMessage("Loại thẻ không đúng");
            RuleFor(s => s.VisitorCredentialFrontImageFromRequest).NotNull().NotEmpty();
            RuleFor(s => s.Email).NotNull().NotEmpty().EmailAddress().WithMessage("Email không hợp lệ");
            RuleFor(s => s.VisitorCredentialBackImageFromRequest).NotNull().NotEmpty();
            RuleFor(s => s.VisitorCredentialBlurImageFromRequest).NotNull().NotEmpty();
        }
    }
}
