using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.DomainDTOs
{
    public class ValidateVisitDateDTO
    {
        public DateTime VisitDate { get; set; }
        public TimeSpan TimeIn { get; set; }
        public TimeSpan TimeOut { get; set; }
        public int? VisitId { get; set; }
    }
}
