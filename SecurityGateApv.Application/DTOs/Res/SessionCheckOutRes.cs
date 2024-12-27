using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class SessionCheckOutRes
    {
        public int VisitorSessionId { get; set; }
        public DateTime CheckinTime { get; set; }
        //public DateTime? CheckoutTime { get; set; }
        public List<VisitorSessionImageRes> VisitorSessionsImages { get; set; }
        public VisitDetailSessionRes VisitDetail { get; set; }
        public SecurityRes SecurityIn { get; set; }
        //public SecurityRes? SecurityOut { get; set; }
        public GateRes GateIn { get; set; }
        //public GateRes? GateOut { get; set; }
        public string Status { get; set; }
        public VisitCardRes VisitCard { get; set; }
        public VehicleSessionRes VehicleSession { get; set; }
    }
    public class VisitDetailSessionRes
    {
        public int VisitDetailId { get; set; }
        public TimeSpan ExpectedStartHour { get; private set; }
        public TimeSpan ExpectedEndHour { get; private set; }
        public VisitorRes Visitor { get; set; }
        public VisitRes Visit { get; set; }
        //public bool Status { get; private set; }
    }
}
