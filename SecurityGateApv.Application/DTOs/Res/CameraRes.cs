using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class CameraRes
    {
        public int Id { get; set; }
        public string CameraURL { get;  set; }
        public string Description { get;  set; }
        public bool Status { get;  set; }
        public int GateId { get;  set; }

        public CameraTypeRes CameraType { get;  set; }
    }
}
