#region Using

using System.Threading.Tasks;
using BERlogic.CallCenter.Models;

#endregion

namespace BERlogic.CallCenter.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message, MailServerConfiguration configuration);
        Task<bool> SendEmailAsync(string email, string subject, string message, MailServerConfiguration configuration,
                            byte[] attachment, string fileName);
    }
}
