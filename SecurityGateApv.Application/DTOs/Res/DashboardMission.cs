using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class DashboardMission
    {
        public int Total {  get; set; }
        public int Pending { get; set; }
        public int Approved { get; set; }
        public int Assigned { get; set; }
        public int Rejected { get; set; }
        public int Expired { get; set; }
        public int Cancel { get; set; }
    }
}
