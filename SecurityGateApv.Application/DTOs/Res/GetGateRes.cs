using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetGateRes
    {
        public int GateId { get; set; }
        public string GateName { get; set; }
        public DateTime CreateDate { get; private set; }
        public string Description { get; private set; }
        public bool Status { get; private set; }
        public ICollection<CameraRes> Cameras { get; private set; }

    }
    //public class CameraRes
    //{
    //    public int Id { get; set; }
    //    public string CaptureURL { get; private set; }
    //    public string StreamURL { get; private set; }
    //    public string Description { get; private set; }
    //    public bool Status { get; private set; }
    //    public int CameraTypeId { get; private set; }
    //}
}
