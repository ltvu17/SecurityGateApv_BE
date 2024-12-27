using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class VisitorSessionCountMonthRes
    {
        public List<DailyCount> DailyCounts { get; set; } = new List<DailyCount>();

    }
    public class DailyCount
    {
        public int Day { get; set; }
        public int Count { get; set; }
    }
}
