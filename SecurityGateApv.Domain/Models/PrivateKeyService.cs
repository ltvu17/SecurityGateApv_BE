using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class PrivateKeyService
    {
        [Key]
        public int PrivateKeyServiceId { get; set; }
        public string KeyName { get; private set; }
        public string Key { get; private set; }
        public bool Status { get; private set; }
    }
}
