using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Interfaces.DomainDTOs
{
    public class AWSDomainDTO
    {
        public string Label { get; set; }
        public float Confidence { get; set; }
        public List<string>? Colors { get; set; }
    }
}
