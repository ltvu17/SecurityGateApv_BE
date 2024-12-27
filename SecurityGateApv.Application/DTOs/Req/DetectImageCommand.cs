using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class DetectImageCommand
    {
        public IFormFile Image { get; set; }
    }
}
