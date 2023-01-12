using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IMailClientConfiguration
    {
        IQueryable<MailClientConfiguration> MailClientConfigurations { get; }
        void SaveConfiguration(MailClientConfiguration configuration);
        MailClientConfiguration DeleteConfiguration(int Id);
    }
}
