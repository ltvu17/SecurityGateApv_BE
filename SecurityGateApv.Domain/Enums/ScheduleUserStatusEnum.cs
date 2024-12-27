using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Enums
{
    public enum ScheduleUserStatusEnum
    {
        None ,
        Assigned,
        Pending,
        Approved,
        Rejected,
        Expired,
        Cancel,
    }
}
