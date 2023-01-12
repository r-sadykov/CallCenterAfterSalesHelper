using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IMailServerConfiguration
    {
        IQueryable<MailServerConfiguration> MailServerConfigurations { get; }
        void SaveConfiguration(MailServerConfiguration configuration);
        MailServerConfiguration DeleteConfiguration(int Id);
    }
}
