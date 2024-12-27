using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetUserRes
    {
        public int UserId { get; set; }
        public string UserName { get; private set; }
        public string FullName { get; private set; }
        public string Email { get; private set; }
        public string PhoneNumber { get; private set; }
        public string? Image { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public string Status { get; private set; }
        public RoleRes Role { get; private set; }
        public DeparmentRes Department { get; private set; }
        public int UserMission { get; set; }
    }

    public class RoleRes
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
    }
    public class DeparmentRes
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
