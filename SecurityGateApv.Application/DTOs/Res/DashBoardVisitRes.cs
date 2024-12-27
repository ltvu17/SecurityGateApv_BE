using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class DashBoardVisitRes
    {
        public int Total {  get; set; }
        public int Daily { get; set; }
        public int Week {  get; set; }
        public int Month { get; set; }
        public int Cancel { get; set; }
        public int Violation { get; set; }
        public int ViolationResolved { get; set; }
        public int Active { get; set; }
        public int Inactive { get; set; }
        public int Pending { get; set; }
        public int ActiveTemporary { get; set; }
        //public int ViolationResolved { get; set; }
    }
}
