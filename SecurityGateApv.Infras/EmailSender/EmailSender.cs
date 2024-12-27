
using Microsoft.AspNetCore.Identity.UI.Services;
using SecurityGateApv.Domain.Interfaces.EmailSender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using IEmailSender = SecurityGateApv.Domain.Interfaces.EmailSender.IEmailSender;

namespace SecurityGateApv.Infras.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string text)
        {
            try
            {
                MailMessage message = new MailMessage();
                message.From = new MailAddress("luutranvu123qb@gmail.com", "APVSecurity");
                message.To.Add(email);

                message.Subject = subject;

                message.Body = text;

                SmtpClient client = new SmtpClient("smtp.gmail.com");
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential("luutranvu123qb@gmail.com", "ncuk pzhc eorl kbvs");
                client.Send(message);
            }
            catch(Exception ex) { 
                
            }
            
        }
    }
}
