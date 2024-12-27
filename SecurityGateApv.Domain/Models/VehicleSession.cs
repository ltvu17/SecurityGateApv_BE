using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class VehicleSession
    {
        public VehicleSession()
        {
            
        }
        internal VehicleSession(string licensePlate, int visitorSesionId)
        {
            LicensePlate = licensePlate;
            VisitorSessionId = visitorSesionId;
        }
        public VehicleSession(string licensePlate, VisitorSession visitorSesion)
        {
            LicensePlate = licensePlate;
            VisitorSession = visitorSesion;
        }
        [Key]
        public int VehicleSessionId { get; set; }
        public string LicensePlate { get; private set; }

        [ForeignKey("VisitorSession")]
        public int VisitorSessionId { get; private set; }
        public VisitorSession VisitorSession { get; private set; }

        public ICollection<VehicleSessionImage> Images { get; private set; } = new List<VehicleSessionImage>();
        public static Result<VehicleSession> Checkin(string licensePlate, int visitorSesionId)
        {
            var visitorSession = new VehicleSession(licensePlate, visitorSesionId);

            return Result.Success(visitorSession);
        }
        public static Result<VehicleSession> Checkin(string licensePlate, VisitorSession visitorSesion)
        {
            var visitorSession = new VehicleSession(licensePlate, visitorSesion);

            return Result.Success(visitorSession);
        }
        public  Result<VehicleSession> CheckOut( List<(string ImageType, string ImageURL)> images)
        {
           

            foreach (var image in images)
            {
                var imageVisitorSession = new VehicleSessionImage(image.ImageType, image.ImageURL, this);
                Images.Add(imageVisitorSession);
            }
            return Result.Success(this);
        }
        public Result<VehicleSession> AddVehicleSessionImage(string imageType, string imageURL)
        {
            var imageVisitorSession = new VehicleSessionImage(imageType, imageURL, this);
            Images.Add(imageVisitorSession);
            return this;
        }
    }
}
