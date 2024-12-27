using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.DomainDTOs
{
    public class UserConnectionDTO
    {
        public int UserId { get; set; }
        public string Role {  get; set; }
    }
}
