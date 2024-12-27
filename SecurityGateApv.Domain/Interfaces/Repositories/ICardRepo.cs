using Microsoft.AspNetCore.Http;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.Repositories
{
    public interface ICardRepo : IRepoBase<Card>
    {
        public Task<Card> GenerateQRCard(string cardIdGuid, IFormFile file, string cardTypeName );
    }
}
