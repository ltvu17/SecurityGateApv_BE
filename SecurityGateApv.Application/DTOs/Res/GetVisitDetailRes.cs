using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetVisitDetailRes
    {
        public int VisitDetailId { get; set; }
        public TimeSpan ExpectedStartHour { get; private set; }
        public TimeSpan ExpectedEndHour { get; private set; }
        public bool Status { get; private set; }
        public string SessionStatus { get;  set; }
        public VisitorRes Visitor { get; set; }
    }

    public class VisitorRes
    {
        public int VisitorId { get; set; }
        public string VisitorName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string CredentialsCard { get; set; }
        public string VisitorCredentialFrontImage { get; set; }
        public string Status { get; set; }
    }
    public class GetVisitByCredentialCardRes
    {
        public int VisitDetailId { get; set; }
        public TimeSpan ExpectedStartHour { get; private set; }
        public TimeSpan ExpectedEndHour { get; private set; }
        public bool Status { get; private set; }
        public VisitorRes Visitor { get; set; }
        public VisitRes Visit { get; set; }
    }
    public class VisitRes
    {
        public int VisitId { get; set; }
        public string VisitName { get; set; }
        public int VisitQuantity { get; set; }
        public string CreateByname { get; set; }
        public string ScheduleTypeName { get; set; }
        public string VisitStatus { get; set; }

    }
}
