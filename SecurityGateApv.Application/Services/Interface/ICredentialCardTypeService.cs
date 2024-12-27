using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services.Interface
{
    public interface ICredentialCardTypeService
    {
        public Task<Result<IEnumerable<CredentialCardType>>> GetAll();
    }
}
