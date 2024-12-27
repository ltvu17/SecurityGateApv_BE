using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.CreateReq
{
    public class CreateGateCommand
    {
        public string GateName { get; set; }
        public string Description { get; set; }
        public ICollection<CameraCommand> Cameras { get;  set; }
    }
    public class CameraCommand
    {
        public string CameraURL { get; set; }
        public string Description { get;  set; }
        [JsonIgnore]
        public bool Status { get; set; }

        public int CameraTypeId { get; set; }
    }
}
