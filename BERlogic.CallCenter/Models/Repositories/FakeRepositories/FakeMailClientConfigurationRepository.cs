using System;
using System.Collections.Generic;
using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories.FakeRepositories
{
    public class FakeMailClientConfigurationRepository:IMailClientConfiguration
    {
        public IQueryable<MailClientConfiguration> MailClientConfigurations => new List<MailClientConfiguration>
        {
            new MailClientConfiguration{ConfigurationId = 1, ConfigurationName = "Mail Client 1",
                SourceFolder = "GermanWings FTC", TargetFolder = "Completed Mails"},
            new MailClientConfiguration{ConfigurationId = 2, ConfigurationName = "Mail Client 2",
                SourceFolder = "Alliance Travel FTC", TargetFolder = "Russian Looked Mails"}
        }.AsQueryable();

        public void SaveConfiguration(MailClientConfiguration configuration)
        {
            throw new NotImplementedException();
        }

        public MailClientConfiguration DeleteConfiguration(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
