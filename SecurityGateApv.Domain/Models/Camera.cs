using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class Camera
    {
        [Key]
        public int Id { get; set; }
        public string CameraURL { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }


        [ForeignKey("Gate")]
        public int GateId { get; private set; }
        public Gate Gate { get; private set; }


        [ForeignKey("CameraType")]
        public int CameraTypeId { get; private set; }
        public CameraType CameraType { get; private set; }

        private Camera(string cameraURL, string description, bool status, int gateId, int cameraTypeId)
        {
            CameraURL = cameraURL;
            Description = description;
            Status = status;
            GateId = gateId;
            CameraTypeId = cameraTypeId;
        }
        public Camera(string cameraURL, string description, bool status, int cameraTypeId, Gate gate)
        {
            CameraURL = cameraURL;
            Description = description;
            Status = status;
            Gate = gate;
            CameraTypeId = cameraTypeId;
        }
        private Camera(int cameraId, string cameraURL, string description, bool status, int gateId, int cameraTypeId)
        {
            Id = cameraId;
            CameraURL = cameraURL;
            Description = description;
            Status = status;
            GateId = gateId;
            CameraTypeId = cameraTypeId;
        }
        public static Result<Camera> Create(string cameraURL, string description, bool status, int gateId, int cameraTypeId)
        {
            var camera = new Camera(cameraURL, description, status, gateId, cameraTypeId);
            return Result.Success(camera);

        }
        public static Result<Camera> Create(int cameraId, string cameraURL, string description, bool status, int gateId, int cameraTypeId)
        {
            var camera = new Camera(cameraId, cameraURL, description, status, gateId, cameraTypeId);
            return Result.Success(camera);

        }
        public Result<Camera> Update(string cameraURL, string description, bool status, int gateId, int cameraTypeId)
        {
            CameraURL = cameraURL;
            Description = description;
            Status = status;
            GateId = gateId;
            CameraTypeId = cameraTypeId;
            return Result.Success(this);
        }
    }
}
