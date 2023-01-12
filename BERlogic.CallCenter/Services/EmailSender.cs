#region Using

using System;
using System.Threading.Tasks;
using BERlogic.CallCenter.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Utils;

#endregion

namespace BERlogic.CallCenter.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message, MailServerConfiguration configuration)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(configuration.ServerNameOfOutcomeMessages, configuration.ServerPortOfOutcomeMessages, SecureSocketOptions.Auto);
                client.Authenticate(configuration.Address,configuration.Password);
                var mail = new MimeMessage();
                mail.From.Add(new MailboxAddress(configuration.UserName, configuration.Address));
                mail.To.Add(new MailboxAddress(email));
                mail.MessageId = MimeUtils.GenerateMessageId();
                mail.Date=DateTimeOffset.Now;
                mail.Subject = subject;
                var builder=new BodyBuilder();
                builder.HtmlBody += message;
                builder.TextBody += message;
                mail.Body = builder.ToMessageBody();
                client.Send(mail);
            }
            return Task.CompletedTask;
        }

        public Task<bool> SendEmailAsync(string email, string subject, string message, MailServerConfiguration configuration, byte[] attachment, string fileName)
        {
            using (var client = new SmtpClient())
            {
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect(configuration.ServerNameOfOutcomeMessages, configuration.ServerPortOfOutcomeMessages, SecureSocketOptions.Auto);
                //client.Connect(configuration.ServerNameOfOutcomeMessages, configuration.ServerPortOfOutcomeMessages, (SecureSocketOptions)configuration.ServerSecureConnectionParameters);
                client.Authenticate(configuration.Address, configuration.Password);
                var mail = new MimeMessage();
                mail.From.Add(new MailboxAddress(configuration.UserName, configuration.Address));
                mail.To.Add(new MailboxAddress(email));
                mail.Bcc.Add(new MailboxAddress(configuration.UserName, configuration.Address));
                mail.MessageId = MimeUtils.GenerateMessageId();
                mail.Date = DateTimeOffset.Now;
                mail.Subject = subject;
                var builder = new BodyBuilder();
                builder.HtmlBody += message;
                builder.TextBody += message;
                fileName = fileName + ".pdf";
                if (attachment!=null)
                {
                    builder.Attachments.Add(fileName, attachment, new ContentType("application", "pdf"));
                }
                mail.Body = builder.ToMessageBody();
                mail.Importance = MessageImportance.High;
                mail.Priority = MessagePriority.Urgent;
                try
                {
                    client.Send(mail);
                    return Task.FromResult(true);
                }
                catch (Exception)
                {
                    return Task.FromResult(false);
                }
            }
        }
    }
}
