using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.UpdateReq
{
    public class UpdateUserPasswordCommand
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordCheck { get; set; }
    }
}
