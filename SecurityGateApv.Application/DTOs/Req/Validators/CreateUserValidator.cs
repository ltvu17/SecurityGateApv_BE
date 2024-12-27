﻿using FluentValidation;
using SecurityGateApv.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.Validators
{
    public class CreateUserValidator: AbstractValidator<CreateUserComman>
    {
        public CreateUserValidator(IUserRepo userRepo, IDepartmentRepo departmentRepo, IRoleRepo roleRepo)
        {
            RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("Bắt buộc nhập tên đăng nhập")
            .MinimumLength(3).WithMessage("Tên đăng nhập phải lớn hơn 3 kí tự")
            .Must( s =>
            {
                return !userRepo.IsAny(x => x.UserName == s).GetAwaiter().GetResult();

            }).WithMessage("Tên đăng nhập này đã tồn tại")
            ;

            RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Bắt buộc nhập mật khẩu")
            .MinimumLength(6).WithMessage("Mật khẩu lớn hơn 6 chữ số");

            RuleFor(x => x.Image).NotNull()
            .NotEmpty().WithMessage("Bắt buộc đưa vào ảnh người dùng");


            RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Bắt buộc nhập tên")
            .Matches(@"^[A-Za-zàáãạảăắằẳẵặâấầẩẫậèéẹẻẽêềếểễệđìíĩỉịòóõọỏôốồổỗộơớờởỡợùúũụủưứừửữựỳỵỷỹýÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬÈÉẸẺẼÊỀẾỂỄỆĐÌÍĨỈỊÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢÙÚŨỤỦƯỨỪỬỮỰỲỴỶỸÝ]+(?:[-'\s.][A-Za-zàáãạảăắằẳẵặâấầẩẫậèéẹẻẽêềếểễệđìíĩỉịòóõọỏôốồổỗộơớờởỡợùúũụủưứừửữựỳỵỷỹýÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬÈÉẸẺẼÊỀẾỂỄỆĐÌÍĨỈỊÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢÙÚŨỤỦƯỨỪỬỮỰỲỴỶỸÝ]+)+$").WithMessage("Tên người dùng chỉ bao gồm chữ và 2 từ trở lên");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Bắt buộc nhập email")
                .EmailAddress().WithMessage("Email không hợp lệ");

            RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Bắt buộc nhập số điện thoại")
                           .Matches(@"^(0\d{9})$").WithMessage("Số điện thoại phải đúng 10 số và bắt đầu bằng số 0");


            RuleFor(x => x.DepartmentId)
                .NotNull().NotEmpty().Must(s =>
                {
                    return departmentRepo.IsAny(x => x.DepartmentId == s).GetAwaiter().GetResult();
                }).WithMessage("Phòng ban này không tồn tại");

            RuleFor(x => x.RoleID)
            .NotNull().NotEmpty().Must(s =>
            {
                return roleRepo.IsAny(x => x.RoleId == s).GetAwaiter().GetResult();
            }).WithMessage("Vai trò này không tồn tại");
        }
    }
}