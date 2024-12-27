using SecurityGateApv.Application.DTOs.Req.CreateReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.UpdateReq
{
    public class GateUpdateCommand
    {
        public int GateId { get; set; }
        public string GateName { get; set; }
        public string Description { get; set; }
        public ICollection<UpdateCameraCommand> Cameras { get; set; }
    }
    public class UpdateCameraCommand
    {
        public int CameraId { get; set; } 
        public string CameraURL { get; set; }
        public string Description { get; set; } 
        [JsonIgnore]
        public bool Status { get; set; }

        public int CameraTypeId { get; set; }
    }
}
