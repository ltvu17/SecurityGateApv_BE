using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class VisitorSessionImageRes
    {
        public int VisitorSessionsImageId { get; set; }
        public string ImageType { get;  set; }
        public string ImageURL { get;  set; }
    }
}
