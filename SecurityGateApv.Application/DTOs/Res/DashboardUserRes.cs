using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class DashboardUserRes
    {
        public int Admin { get; set; }
        public int Manager { get; set; }
        public int DepartmentManager { get; set; }
        public int Staff { get; set; }
        public int Security { get; set; }
    }
}
