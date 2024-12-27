using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class CameraType
    {
        [Key]
        public int CameraTypeId { get; private set; }
        public string CameraTypeName { get; private set; }
        public string Description { get; private set; }
        public ICollection<Camera> Cameras { get; private set; }

        private CameraType(string cameraTypeName, string description)
        {
            CameraTypeName = cameraTypeName;
            Description = description;
            Cameras = new List<Camera>();
        } 
        private CameraType(int cameraTypeId, string cameraTypeName, string description)
        {
            CameraTypeId = cameraTypeId;
            CameraTypeName = cameraTypeName;
            Description = description;
            Cameras = new List<Camera>();
        }

        public static Result<CameraType> Create(string cameraTypeName, string description)
        {
            var cameraType = new CameraType(cameraTypeName, description);
            return Result.Success(cameraType);
        }
        public static Result<CameraType> Create(int cameraTypeId, string cameraTypeName, string description)
        {
            var cameraType = new CameraType(cameraTypeId, cameraTypeName, description);
            return Result.Success(cameraType);
        }
    }
}
