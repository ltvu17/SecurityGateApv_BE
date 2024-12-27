using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Yêu cầu tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Yêu cầu mật khẩu")]
        public string Password { get; set; }
    }
}
