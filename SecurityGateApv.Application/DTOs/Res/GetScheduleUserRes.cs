using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetScheduleUserRes
    {
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string? Note { get; private set; }
        public DateTime AssignTime { get; private set; }
        public DateTime DeadlineTime { get; private set; }
        public int? MaxPersonQuantity { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public string Status { get; private set; }
        public ScheduleGetScheduleUserRes Schedule { get; private set; }
        public UserGetScheduleUserRes AssignTo { get; private set; }
        public UserGetScheduleUserRes AssignFrom { get; private set; }
    }
    public class ScheduleGetScheduleUserRes
    {
        public int ScheduleId { get; private set; }
        public string ScheduleName { get; private set; }
        public string DaysOfSchedule { get; private set; }
        public ScheduleTypeGetScheduleUserRes ScheduleType { get; private set; }
    }
    public class ScheduleTypeGetScheduleUserRes
    {
        public int ScheduleTypeId { get; set; }
        public string ScheduleTypeName { get; set; }
    }
    public class UserGetScheduleUserRes
    {
        public int UserId { get; set; }
        public string FullName { get; private set; }
    }
}
