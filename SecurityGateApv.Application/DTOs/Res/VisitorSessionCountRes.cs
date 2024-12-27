using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class VisitorSessionCountRes
    {
        public List<MonthlyCount> MonthlyCounts { get; set; } = new List<MonthlyCount>();
    }

    public class MonthlyCount
    {
        public int Month { get; set; }
        public int Count { get; set; }
    }
}
