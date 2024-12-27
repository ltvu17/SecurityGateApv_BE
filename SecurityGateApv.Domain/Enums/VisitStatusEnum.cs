using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Enums
{
    public enum VisitStatusEnum
    {
        NONE,
        Pending,
        Active,
        Inactive,
        ActiveTemporary,
        Violation,
        Cancelled,
        ViolationResolved
    }
}
