using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class ValidCheckinRes
    {
        public int VisitDetailId { get; set; }
        public TimeSpan ExpectedStartHour { get; private set; }
        public TimeSpan ExpectedEndHour { get; private set; }
        public bool Status { get; private set; }
        public VisitorRes Visitor { get; set; }
        public VisitRes Visit { get; set; }
        public CardRes CardRes { get; set; }
        public AWSDomainDTO DetectShoeRes { get; set; }
        public SessionsRes Sessions { get; set; }
    }
    public class CardRes
    {
        public int CardId { get; set; }
        public string CardVerification { get; set; }
        public string CardImage { get; set; }
        public string CardStatus { get; set; }
        public string QrCardTypename { get; set; }
    }

}
