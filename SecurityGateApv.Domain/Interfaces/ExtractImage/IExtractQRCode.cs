using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.ExtractImage
{
    public interface IExtractQRCode
    {
        public string ExtractQrCodeFromImage(IFormFile image);
    }
}
