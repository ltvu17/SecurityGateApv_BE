using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class LoginRes
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }
    }
}
