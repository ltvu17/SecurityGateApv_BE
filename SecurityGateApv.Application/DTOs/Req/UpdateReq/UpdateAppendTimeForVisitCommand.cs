using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.UpdateReq
{
    public class UpdateAppendTimeForVisitCommand
    {
        public DateTime ExpectedEndTime { get; set; }
        public int UpdateById { get; set; } 
    }
}
