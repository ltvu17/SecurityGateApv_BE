using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class CreateUserComman
    {
        public string UserName { get;  set; }
        public string Password { get;  set; }
        public string FullName { get;  set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Image { get;  set; }
        public int RoleID { get; set; }
        public int DepartmentId { get; set; }
    }
}
