using System.Net.Mail;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace MyAutoService.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("hhajtaleby22@gmail.com");
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = htmlMessage;
            
            mail.IsBodyHtml = true;

            //System.Net.Mail.Attachment attachment;
            // attachment = new System.Net.Mail.Attachment("c:/textfile.txt");
            // mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("hhajtaleby22@gmail.com", "750057351");
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Send(mail);

            return Task.CompletedTask;
        }
    }
}
