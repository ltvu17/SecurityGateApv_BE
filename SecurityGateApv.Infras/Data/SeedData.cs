using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Infras.Data
{
    public class SeedData
    {
        public static List<Role> GetRoles()
        {
            return new List<Role>
        {
            new Role { RoleId = 1, RoleName = "Admin", Description = "Quản lý toàn bộ hệ thống" },
            new Role { RoleId = 2, RoleName = "Manager", Description = "Quản lý toàn bộ công ty" },
            new Role { RoleId = 3, RoleName = "DepartmentManaager", Description = "Quản lý toàn bộ phòng ban" },
            new Role { RoleId = 4, RoleName = "Staff", Description = "Tạo và quản lý khách ra vào của phòng ban" },
            new Role { RoleId = 5, RoleName = "Security", Description = "Quản lý khách ra vào tại cổng" }
        };
        }
    }
}
