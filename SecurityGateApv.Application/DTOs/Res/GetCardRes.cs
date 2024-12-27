using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class GetCardRes
    {
        public int CardId { get; set; }
        public string CardVerification { get;  set; }
        public DateTime CreateDate { get;  set; }
        public DateTime LastCancelDate { get;  set; }
        public string CardImage { get;  set; }
        public string CardStatus { get;  set; }
        public string QrCardTypename { get;  set; }
    }
}
