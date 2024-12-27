using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetScheduleTypeRes
    {
        public int ScheduleTypeId { get; set; }
        public string ScheduleTypeName { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
    }
}
