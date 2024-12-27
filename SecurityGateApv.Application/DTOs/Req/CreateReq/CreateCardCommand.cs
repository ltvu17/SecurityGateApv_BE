using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.CreateReq
{
    public class CreateCardCommand
    {
        public string CardVerified { get; set; }
        public int CardTypeId { get; set; }
        public IFormFile ImageLoGo { get; set; }
    }
}
