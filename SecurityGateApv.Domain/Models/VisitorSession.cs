using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityGateApv.Domain.Shared;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Enums;
using System.Runtime.CompilerServices;

namespace SecurityGateApv.Domain.Models
{
    public class VisitorSession
    {
        public VisitorSession()
        {
            
        }
        internal VisitorSession(DateTime checkin, DateTime? checkout, int visitDetailId, int securityInId, int? securityOutId, int gateIn, int? gateOut, string status)
        {
            CheckinTime = checkin;
            CheckoutTime = checkout;
            VisitDetailId = visitDetailId;
            SecurityInId = securityInId;
            SecurityOutId = securityOutId;
            GateInId = gateIn;
            GateOutId = gateOut;
            Status = status;
        }
        [Key]
        public int VisitorSessionId { get;  set; }
        public DateTime CheckinTime { get; private set; }
        public DateTime? CheckoutTime { get; private set; }
        public string Status { get; private set; }


        [ForeignKey("VisitDetail")]
        public int VisitDetailId { get; private set; }
        public VisitDetail VisitDetail { get; private set; }


        [ForeignKey("SecurityIn")]
        public int SecurityInId { get; private set; }
        public User SecurityIn { get; private set; }


        [ForeignKey("SecurityOut")]
        public int? SecurityOutId { get; private set; }
        public User? SecurityOut { get; private set; }


        [ForeignKey("GateIn")]
        public int GateInId { get; private set; }
        public Gate GateIn { get; private set; }

        [ForeignKey("GateOut")]
        public int? GateOutId { get; private set; }
        public Gate? GateOut { get; private set; }

        public ICollection<VisitorSessionsImage> VisitorSessionsImages { get; private set; } = new List<VisitorSessionsImage>();
        public VehicleSession VehicleSession { get; private set; } 


        public static Result<VisitorSession> Checkin( int visitdetailId, int securityInId, int gateInId)
        {
            var visitorSession = new VisitorSession(DateTime.Now, null, visitdetailId, securityInId, null, gateInId, null, SessionStatus.CheckIn.ToString());

            return Result.Success(visitorSession);
        }
        public Result<VisitorSession> CheckOutMock( int securityOutId, int gateOutId)
        {
            this.SecurityOutId = securityOutId;
            this.GateOutId = gateOutId;
            this.Status = SessionStatus.CheckOut.ToString();
            this.CheckoutTime = DateTime.Now;

            return this;
        }
        //public static Result<VisitorSession> AddVehicleCheckin(string licensePlate, VisitorSession visitorSession)
        //{
        //    var vehicleSession = new VehicleSession(licensePlate, visitorSession);
        //    visitorSession.VehicleSession.Add(vehicleSession);

        //    return Result.Success(visitorSession);
        //}
        public Result<VisitorSession> AddVisitorImage(string imageType, string imageURL)
        {
            var imageVisitorSession = new VisitorSessionsImage(imageType, imageURL, this);
            VisitorSessionsImages.Add(imageVisitorSession);
            return this;
        }
        /*public Result<VisitorSession> UpdateVisitorSesson()
        {
            var visitorSesson = new VisitorSession
            
            return this;
        }*/
    }
}
