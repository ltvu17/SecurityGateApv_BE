using SecurityGateApv.Domain.Enums;
using SecurityGateApv.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Domain.Models
{
    public class Card
    {
        [Key]
        public int CardId { get; set; }
        public string CardVerification { get; private set; }
        public DateTime CreateDate { get; private set; }
        public DateTime LastCancelDate { get; private set; }
        public string CardImage { get; private set; }
        public string CardStatus { get; private set; }

        [ForeignKey("QRCardType")]
        public int CardTypeId { get; private set; }
        public CardType CardType { get; private set; }

        public ICollection<VisitCard> VisitCards { get; private set; }
       /* public Result<QRCard> Update(Guid cardGuidId)
        {
            this.CardGuidId = cardGuidId;
            return this;
        }*/
        public Result<Card> UpdateQRCardStatus(string status)
        {
            this.CardStatus = status;
            return this;
        }
        public static Card Create(int qrCardTypeId,  string cardVerified, string cardImage)
        {
            var qrCard = new Card
            {
                CardVerification = cardVerified,
                CreateDate = DateTime.Now,
                LastCancelDate = DateTime.Now,
                CardImage = cardImage,
                CardStatus = CardStatusEnum.Active.ToString(),
                CardTypeId = qrCardTypeId,
            };

            return qrCard;
        }
        public static Result<Card> GenerateCard( string cardVerified, string cardImage)
        {
            var qrCard = new Card
            {
                CardVerification = cardVerified,
                CreateDate = DateTime.Now,
                LastCancelDate = DateTime.Now,
                CardImage = cardImage,
                CardStatus = CardStatusEnum.Active.ToString(),
                //CardTypeId = qrCardTypeId,
            };

            return qrCard;
        }
    }
}
