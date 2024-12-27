using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class VehicleSessionImage
    {
        public VehicleSessionImage() { }
        public VehicleSessionImage(string imageType, string imageURL, VehicleSession vehicleSession)
        {
            ImageType = imageType;
            ImageURL = imageURL;
            VehicleSession = vehicleSession;
        }
        [Key]
        public int VehicleSessionImageId { get; set; }
        public string ImageType { get; set; }
        public string ImageURL { get; set; }

        [ForeignKey("VehicleSession")]
        public int VehicleSessionId { get; set; }
        public VehicleSession VehicleSession { get; set; }

    }
}
