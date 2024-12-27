using SecurityGateApv.Application.Services.Interface;
using SecurityGateApv.Domain.Errors;
using SecurityGateApv.Domain.Interfaces.Repositories;
using SecurityGateApv.Domain.Models;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.Services
{
    public class CredentialCardTypeService : ICredentialCardTypeService
    {
        private readonly ICredentialCardTypeRepo _credentialCardTypeRepo;

        public CredentialCardTypeService(ICredentialCardTypeRepo CredentialCardTypeRepo)
        {
            _credentialCardTypeRepo = CredentialCardTypeRepo;
        }
        public async Task<Result<IEnumerable<CredentialCardType>>> GetAll()
        {
            var cardType = await _credentialCardTypeRepo.GetAllAsync();
            if(cardType.Count() == 0)
            {
                return Result.Failure<IEnumerable<CredentialCardType>>(Error.CardFoundError);
            }
            return cardType.ToList();
        }
    }
}
