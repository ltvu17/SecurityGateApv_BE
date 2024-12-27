using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetDepartmentRes
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int AcceptLevel { get; set; }
    }
}
