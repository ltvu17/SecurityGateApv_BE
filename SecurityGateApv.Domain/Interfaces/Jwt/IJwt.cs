using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.Jwt
{
    public interface IJwt
    {
        public string GenerateJwtToken(User user);
        public string DecodeJwt(string header);
        public int DecodeJwtUserId(string header);
        public UserAuthorDTO DecodeAuthorJwt(string header);
    }
}
