using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class ScheduleUserRes
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string? Note { get; private set; }
        public DateTime AssignTime { get; private set; }
        public DateTime DeadlineTime { get; private set; }
        public string Status { get; private set; }
        public int ScheduleId { get; private set; }
        public int AssignToId { get; private set; }
        public UserScheduleUserRes AssignFrom { get; private set; }
        public ScheduleRes Schedule { get; private set; }
        public List<VisitRes> Visit { get; private set; }
    }
    public class ScheduleRes
    {
        public int ScheduleId { get; private set; }
        public string DaysOfSchedule { get; set; }
        public ScheduleTypeRes ScheduleType { get; set; }

    } 
    public class ScheduleTypeRes
    {
        public int ScheduleTypeId { get; set; }
        public string ScheduleTypeName { get; set; }

    }
    public class UserScheduleUserRes
    {
        public int UserId { get; set; }
        public string UserName { get; private set; }
    }
}
