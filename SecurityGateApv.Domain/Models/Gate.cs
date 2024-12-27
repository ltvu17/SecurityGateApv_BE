using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SecurityGateApv.Domain.Models
{
    public class Gate
    {
        [Key]
        public int GateId { get; private set; }
        public string GateName { get; private set; }
        public DateTime CreateDate { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }


        //public string CameraURL { get; set; }
        public ICollection<Camera> Cameras { get; private set; }

        public ICollection<VisitorSession> VisitorSessionsIn { get; private set; }
        public ICollection<VisitorSession> VisitorSessionsOut { get; private set; }



        private Gate(string gateName, DateTime createDate, string description, bool status)
        {
            GateName = gateName;
            CreateDate = createDate;
            Description = description;
            Status = status;
            Cameras = new List<Camera>();
            VisitorSessionsIn = new List<VisitorSession>();
            VisitorSessionsOut = new List<VisitorSession>();
        }
        private Gate(int gateId, string gateName, DateTime createDate, string description, bool status)
        {
            GateId = gateId;
            GateName = gateName;
            CreateDate = createDate;
            Description = description;
            Status = status;
            Cameras = new List<Camera>();
            VisitorSessionsIn = new List<VisitorSession>();
            VisitorSessionsOut = new List<VisitorSession>();
        }

        public static Result<Gate> Create(string gateName, DateTime createDate, string description, bool status)
        {
            var gate = new Gate(gateName, createDate, description, status);
            return Result.Success(gate);
        }
        public Result<Gate> Update(string gateName,  string description, bool status)
        {
            GateName = gateName;
            Description = description;
            Status = status;
            return Result.Success(this);
        }
        public Result<Gate> Delete()
        {
            Status = false;
            return Result.Success(this);
        }
        public static Result<Gate> Create(int gateId, string gateName, DateTime createDate, string description, bool status)
        {
            var gate = new Gate(gateId, gateName, createDate, description, status);
            return Result.Success(gate);
        }
        public Result<Gate> AddCamera(string cameraURL, string description, bool status, int cameraTypeId)
        {
            var camera = new Camera(cameraURL, description, status, cameraTypeId, this);
            Cameras.Add(camera);
            return this;
        }
    }
}
