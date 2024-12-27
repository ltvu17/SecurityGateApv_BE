using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req.UpdateReq
{
    public class UpdateVisitBeforeStartDateCommand
    {
        public string VisitName { get; set; }
        public int VisitQuantity { get; set; }
        public DateTime ExpectedStartTime { get; set; }
        public DateTime ExpectedEndTime { get; set; }
        public int UpdateById {  get; set; }  
        public string Description { get; set; }
        public ICollection<VisitDetailOldCommand> VisitDetail { get; set; }
    }
}
