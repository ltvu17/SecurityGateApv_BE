using SecurityGateApv.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityGateApv.Application.DTOs.Res
{
    public class VisitCardRes
    {
        public int VisitCardId { get; private set; }
        public DateTime IssueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        public string VisitCardStatus { get; private set; }
        public int VisitDetailId { get; private set; }
        public CardRes? Card { get; private set; }
        public int? CardId { get; private set; }
    }
}
