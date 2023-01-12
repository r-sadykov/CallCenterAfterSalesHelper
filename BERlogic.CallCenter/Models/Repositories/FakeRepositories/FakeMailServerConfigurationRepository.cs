using System.Collections.Generic;
using System.Linq;
using BERlogic.CallCenter.Models.Enums;

namespace BERlogic.CallCenter.Models.Repositories.FakeRepositories
{
    public class FakeMailServerConfigurationRepository : IMailServerConfiguration
    {
        public IQueryable<MailServerConfiguration> MailServerConfigurations => new List<MailServerConfiguration>
        {
            new MailServerConfiguration{ConfigurationId = 1, ConfigurationName = "Mail Server 1",
                UserName = "test", Password = "test", Address = "test@test.de",
                ServerPortOfOutcomeMessages = 460, ServerPortOfIncomeMessages = 993,
                ServerNameOfOutcomeMessages = "remote.test.de", ServerNameOfIncomeMessages = "remote.test.de",
                ServerSecureConnectionParameters = SecureConnectionParameters.Ssl, UsedNameForEmailConnection = "test",
                UseSecureAuthentication = false, UseNameAndPassword = false
            },
            new MailServerConfiguration{ConfigurationId = 2, ConfigurationName = "Mail Server 2",
                UserName = "test", Password = "test", Address = "test@test.de",
                ServerPortOfOutcomeMessages = 25, ServerPortOfIncomeMessages = 143,
                ServerNameOfOutcomeMessages = "remote.test.ru", ServerNameOfIncomeMessages = "remote.test.ru",
                ServerSecureConnectionParameters = SecureConnectionParameters.None,
                UseSecureAuthentication = false, UseNameAndPassword = false}
        }.AsQueryable();

        public void SaveConfiguration(MailServerConfiguration configuration)
        {
        }

        public MailServerConfiguration DeleteConfiguration(int Id)
        {
            return null;
        }
    }
}
