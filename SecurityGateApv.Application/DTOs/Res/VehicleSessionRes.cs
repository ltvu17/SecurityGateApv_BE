using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class VehicleSessionRes
    {
        public int VehicleSessionId { get; set; }

        public string LicensePlate { get; set; }
        public string Status { get; private set; }
        public List<VehicleSessionImageRes> Images { get;  set; } 
        public int VisitorSessionId { get; set; }

    }
    public class VehicleSessionImageRes
    {
        public int VehicleSessionImageId { get; set; }
        public string ImageType { get; set; }
        public string ImageURL { get; set; }
    }
}
