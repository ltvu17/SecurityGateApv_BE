using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class VisitCreateCommand
    {
        public string VisitName { get;  set; }
        public int VisitQuantity { get;  set; }
        public DateTime ExpectedStartTime { get;  set; }
        public DateTime ExpectedEndTime { get;  set; }
        public int CreateById { get; set; }
        public string? Description { get; set; }
        public int ScheduleUserId { get;  set; }
        public int ResponsiblePersonId { get; set; }
        public ICollection<VisitDetailOldCommand> VisitDetail { get; set; }
    }
    public class VisitCreateCommandDaily
    {
        public string VisitName { get; set; }
        public int VisitQuantity { get; set; }
        public DateTime ExpectedStartTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int CreateById { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public int? ScheduleId { get; set; }
        public int ResponsiblePersonId { get; set; }

        public ICollection<VisitDetailOldCommand> VisitDetail { get; set; }
    }
    public class VisitProjectCommand
    {
        public string VisitName { get; set; }
        public int QuantityOfVisit { get; set; }
        public DateTime ExpectedTimeIn { get; set; }
        public DateTime ExpectedTimeOut { get; set; }
        public int ProjectId { get; set; }
    }
    public class VisitDetailOldCommand
    {
        //public string VisitDetailName { get; set; }
        public TimeSpan ExpectedStartHour { get; set; }
        public TimeSpan ExpectedEndHour { get; set; }
        public int VisitorId { get; set; }
        [JsonIgnore]
        public bool Status { set; get; } = true;
    }
}
