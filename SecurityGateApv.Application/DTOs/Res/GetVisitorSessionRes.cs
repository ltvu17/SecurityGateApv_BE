using FluentValidation;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetVisitorSessionRes
    {
        public int VisitorSessionId { get; set; }
        public DateTime CheckinTime { get;  set; }
        public DateTime? CheckoutTime { get;  set; }
        public int QRCardId { get;  set; }
        public GetVisitDetailRes VisitDetail { get;  set; }
        public SecurityRes SecurityIn { get;  set; }
        public SecurityRes? SecurityOut { get;  set; }
        public GateRes GateIn { get;  set; }
        public GateRes? GateOut { get;  set; }
        public string Status { get;  set; }
        public ICollection<SessionsImageRes> Images { get; private set; }
        public bool IsVehicleSession { get;  set; }

    }
    public class SecurityRes
    {
        public int UserId { get; set; }
        public string FullName { get;  set; }
        public string PhoneNumber { get;  set; }
    }
    public class GateRes
    {
        public int GateId { get; set; }
        public string GateName { get; set; }

    }
    public class SessionsImageRes
    {
        public int VisitorSessionsImageId { get; set; }
        public string ImageType { get; set; }
        public string ImageURL { get; set; }

    }
}
