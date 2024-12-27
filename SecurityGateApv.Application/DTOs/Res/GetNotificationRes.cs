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
    public class GetNotificationRes
    {
        public int NotificationUserID { get; set; }
        public bool ReadStatus { get; set; }
        public int NotificationID { get; set; }
        public GetNotification Notification { get; set; }
        //public int SenderID { get; set; }
        public SenderRes Sender { get; set; }

        public int ReceiverID { get; set; }
    }
    public class SenderRes
    {
        public int UserId { get; set; }
        public string FullName { get; private set; }
        public string? Image { get; private set; }
    }
    public class GetNotification
    {
        public int NotificationID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SentDate { get; set; }
        public DateTime? ReadDate { get; set; }
        public string? Action { get; set; }
        public bool Status { get; set; }
        public NotificationTypeRes NotificationType { get; set; }
    }
    public class NotificationTypeRes
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
