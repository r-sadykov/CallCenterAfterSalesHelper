#region Using

using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Services;

#endregion

namespace BERlogic.CallCenter.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link, MailServerConfiguration configuration)
        {
            return emailSender.SendEmailAsync(email, "Confirm your email",
                $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>", configuration);
        }
    }
}
