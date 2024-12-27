using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetVisitorSessionGraphQLRes
    {
        public int VisitorSessionId { get; set; }
        public DateTime CheckinTime { get; set; }
        public DateTime? CheckoutTime { get; set; }
        public int QRCardId { get; set; }
        public int VisitDetailId { get; set; }
        public GraphQlGetVisitRes Visit { get; set; }
        public GraphQlVisitorRes Visitor { get; set; }
        public VehicleSessionRes? VehicleSession { get; set; }
        public SecurityRes SecurityIn { get; set; }
        public SecurityRes? SecurityOut { get; set; }
        public GateRes GateIn { get; set; }
        public GateRes? GateOut { get; set; }
        public string Status { get; set; }
        public ICollection<SessionsImageRes> Images { get; private set; }
    }
    public class GraphQlGetVisitRes
    {
        public int VisitId { get; set; }
        public string VisitName { get; private set; }
        public int VisitQuantity { get; private set; }
        public DateTime ExpectedStartTime { get; private set; }
        public DateTime ExpectedEndTime { get; private set; }
        public string? Description { get; private set; }
        public string VisitStatus { get; private set; }
        public int CreateById { get; private set; }
        public int? UpdateById { get; private set; }
        public int? ResponsiblePersonId { get; private set; }

        //public CreateByRes CreateBy { get; private set; }
        ////public CreateByRes? UpdateBy { get; private set; }
        //public CreateByRes? ResponbilityBy { get; private set; }

        //public ScheduleResForVisit Schedule { get; private set; }
        //public ICollection<GraphQLVisitDetailRes> VisitDetail { get; set; }
    }
    public class GraphQLVisitDetailRes
    {
        public int VisitDetailId { get; set; }
        public TimeSpan ExpectedStartHour { get; private set; }
        public TimeSpan ExpectedEndHour { get; private set; }
        public bool Status { get; private set; }
    }
    public class GraphQlVisitorRes
    {
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string CompanyName { get; set; }
        public string CredentialsCard { get; private set; }
        public List<VisitCardRes> VisitCard { get; set; }
    }
}
