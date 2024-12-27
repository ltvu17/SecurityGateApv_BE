using SecurityGateApv.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class CardStatusCountRes
    {
        public string Status { get; set; } // Change the type to string
        public int Count { get; set; }
    }

}
