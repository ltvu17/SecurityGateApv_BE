using Microsoft.AspNetCore.Http;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.AWS
{
    public interface IAWSService
    {
        public Task<ICollection<AWSDomainDTO>> DetectLabelService(IFormFile image, PrivateKeyService key);
    }
}
