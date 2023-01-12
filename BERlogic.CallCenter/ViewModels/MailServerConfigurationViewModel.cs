using System.ComponentModel.DataAnnotations;
using BERlogic.CallCenter.Models;

namespace BERlogic.CallCenter.ViewModels
{
    public class MailServerConfigurationViewModel
    {
        public MailServerConfiguration MailServerConfiguration { get; set; }
        [DataType(DataType.EmailAddress, ErrorMessage = "This email address is not valid")]
        public string EMail { get; set; }
    }
}
