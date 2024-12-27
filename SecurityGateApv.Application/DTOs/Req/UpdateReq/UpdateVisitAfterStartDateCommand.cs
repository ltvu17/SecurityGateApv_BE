using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.UpdateReq
{
    public class UpdateVisitAfterStartDateCommand
    {
        public int VisitQuantity { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int UpdateById { get; set; }
        public ICollection<VisitDetaiUpdateVisitAfterStartDateCommand> VisitDetail { get; set; }
    }
    public class VisitDetaiUpdateVisitAfterStartDateCommand
    {
        //public string VisitDetailName { get; set; }
        public TimeSpan ExpectedStartHour { get; set; }
        public TimeSpan ExpectedEndHour { get; set; }
        public int VisitorId { get; set; }
        public bool Status { set; get; }
    }
}
