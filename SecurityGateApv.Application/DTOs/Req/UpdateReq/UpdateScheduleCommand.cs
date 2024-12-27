using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.UpdateReq
{
    public class UpdateScheduleCommand
    {
        public string ScheduleName { get; set; }
        public string DaysOfSchedule { get; set; }
        public string Description { get; set; }
        public bool Status { get;  set; }
        [JsonIgnore]
        public int ScheduleTypeId { get; set; }
        [JsonIgnore]
        public int CreateById { get; set; }
    }
}
