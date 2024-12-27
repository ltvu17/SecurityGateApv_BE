using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class ValidCheckInCommand
    {
        public int VisitDetailId { get; set; }
        public string QRCardVerification { get; set; }
        public IFormFile ImageShoe { get; set; }

    }
}
