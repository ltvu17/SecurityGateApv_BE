using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class CameraTypeRes
    {
        public int CameraTypeId { get;  set; }
        public string CameraTypeName { get;  set; }
        public string Description { get;  set; }
    }
}
