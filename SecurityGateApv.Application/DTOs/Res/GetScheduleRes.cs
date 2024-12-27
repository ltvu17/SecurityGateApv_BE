using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetScheduleRes
    {
        public int ScheduleId { get;  set; }
        public string ScheduleName { get;  set; }
        public string DaysOfSchedule { get;  set; }
        public string Description { get;  set; }
        public DateTime CreateTime { get;  set; }
        public DateTime UpdateTime { get;  set; } 
        public bool Status { get;  set; }
        public GetScheduleTypeRes ScheduleType { get;  set; }
        public GetUserRes CreateBy { get;  set; }
        public List<ScheduleUserRes> ScheduleUser { get;  set; }

    }
}
