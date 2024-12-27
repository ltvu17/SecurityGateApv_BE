using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Req
{
    public class DepartmentCreateCommand
    {
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public int AcceptLevel { get; set; }
    }
}
