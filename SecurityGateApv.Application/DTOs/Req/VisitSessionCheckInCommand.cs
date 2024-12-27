using Microsoft.AspNetCore.Http;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class VisitSessionCheckInCommand
    {
        //public int QRCardId { get; set; }
        public int VisitDetailId { get; set; }
        public int SecurityInId { get; set; }
        public int GateInId { get; set; }
        public string QRCardVerification {  get; set; }

        public List<VisitorSessionsImageCheckinCommand> Images { get; set; }
        public VehicleSessionComand? VehicleSession { get; set; }
    }
    public class VisitorSessionsImageCheckinCommand
    {
        public string ImageType { get; set; }
        public string ImageURL { get; set; }
        //[JsonIgnore]
        public IFormFile Image { get; set; }
    }
    public class VehicleSessionComand
    {
        public string LicensePlate { get; set; }
        public List<vehicleSessionsImageCheckinCommand> VehicleImages { get; set; }

    }
    public class vehicleSessionsImageCheckinCommand
    {
        public string ImageType { get; set; }
        public string ImageURL { get; set; }
    }

}
