using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.CreateReq
{
    public class CreateScheduleCommand
    {

        public string ScheduleName { get; set; }
        public string DaysOfSchedule { get; set; }
        public string Description { get; set; }
        public int ScheduleTypeId { get; set; }
        public int CreateById { get; set; }

    }
    public class ProcessCreateAndDetailCommand
    {
        public string ProcessName { get; set; }
        public string Description { get; set; }
        //public bool Status { get; set; }
        public int VisitTypeId { get; set; }
        public int CreateBy { get; set; }
        public ICollection<VisitProcessCommand> VisitProject { get; set; }
    }
    public class VisitProcessCommand
    {
        public DateTime ExpectedStartDate { get; set; }
        public DateTime ExpectedEndDate { get; set; }
        public TimeSpan ExpectedStartTime { get; set; }
        public TimeSpan ExpectedEndTime { get; set; }
        public string DaysOfProcess { get; set; }
        public int VisitQuantity { get; set; }
        //public string Status { get; set; }
    }
}
