using SecurityGateApv.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class VisitCard
    {
        private VisitCard(DateTime issueDate, DateTime expiryDate, string visitCardStatus, int visitorId, int cardId)
        {
            IssueDate = issueDate;
            ExpiryDate = expiryDate;
            VisitCardStatus = visitCardStatus;
            VisitorId = visitorId;
            CardId = cardId;
        }
        public VisitCard()
        {
            
        }
        public int VisitCardId { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string VisitCardStatus { get; private set; }

        [ForeignKey("Visitor")]
        public int VisitorId { get; private set; }
        public Visitor Visitor { get; private set; }

        [ForeignKey("Card")]
        public int CardId { get; private set; }
        public Card Card { get; private set; }


        //create function create visit card
        public static VisitCard Create(DateTime issueDate, DateTime expiryDate, string visitCardStatus, int visitorId, int cardId)
        {
            VisitCard visitCard = new VisitCard(issueDate, expiryDate, visitCardStatus, visitorId, cardId);

            return visitCard;
        }
        //Create punction update visit card
        public VisitCard UpdateVisitCardStatus(string visitCardStatus)
        {
            this.VisitCardStatus = visitCardStatus;
            return this;
        }
        public VisitCard CancelCardLost()
        {
            this.VisitCardStatus = VisitCardStatusEnum.Expired.ToString();
            this.Card.UpdateQRCardStatus(CardStatusEnum.Lost.ToString());
            return this;
        }
        public VisitCard UpdateVisitCardStatusBackgroundWoker(string visitCardStatus)
        {
            this.VisitCardStatus = visitCardStatus;
            return this;
        }
    }

}
