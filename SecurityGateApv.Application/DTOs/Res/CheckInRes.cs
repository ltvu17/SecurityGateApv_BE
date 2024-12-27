using SecurityGateApv.Application.DTOs.Req;
using SecurityGateApv.Domain.Interfaces.DomainDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class CheckInRes
    {
        public int VisitDetailId { get; set; }
        public int SecurityInId { get; set; }
        public int GateInId { get; set; }
        public SessionsRes SessionsImageRes { get; set; }
        public GetCardRes Card { get; set; }
        public AWSDomainDTO DetectShoeRes { get; set; }
    }
    public class SessionsRes
    {
        public DateTime CheckinTime { get; private set; }
        public int SecurityInId { get; private set; }
        public int GateInId { get; private set; }
        //public ICollection<VisitorSessionsImageCheckinCommand> Images { get; set; }

    }

    //public class VisitorSessionsImageCheckinCommand
    //{
    //    public string ImageType { get; set; }
    //    public string ImageURL { get; set; }
    //    //public IFormFile Image { get; set; }
    //}
    //public class CardRes
    //{
    //    public int Id { get; set; }
    //    public string CardVerification { get; set; }
    //    public string CardImage { get; private set; }
    //    public string CardTypeName { get; set; }
    //    public string CardStatus { get; set; }
    //}
}
