using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public ICollection<User> User { get; set; }
    }
}
