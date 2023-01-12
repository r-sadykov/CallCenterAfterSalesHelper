using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class MailServerConfigurationRepository:IMailServerConfiguration
    {
        private readonly ApplicationDbContext _context;

        public MailServerConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Implementation of IMailServerConfiguration

        public IQueryable<MailServerConfiguration> MailServerConfigurations => _context.MailServerConfigurations;
        public void SaveConfiguration(MailServerConfiguration configuration)
        {
            if (configuration.ConfigurationId == 0)
            {
                _context.MailServerConfigurations.Add(configuration);
            }
            else
            {
                MailServerConfiguration dbEntry = _context.MailServerConfigurations.FirstOrDefault(p => p.ConfigurationId == configuration.ConfigurationId);
                if (dbEntry != null)
                {
                    dbEntry.ConfigurationName = configuration.ConfigurationName;
                    dbEntry.UserName = configuration.UserName;
                    dbEntry.Address = configuration.Address;
                    dbEntry.Password = configuration.Password;
                    dbEntry.ServerNameOfIncomeMessages = configuration.ServerNameOfIncomeMessages;
                    dbEntry.ServerPortOfIncomeMessages = configuration.ServerPortOfIncomeMessages;
                    dbEntry.ServerNameOfOutcomeMessages = configuration.ServerNameOfOutcomeMessages;
                    dbEntry.ServerPortOfOutcomeMessages = configuration.ServerPortOfOutcomeMessages;
                    dbEntry.ServerSecureConnectionParameters = configuration.ServerSecureConnectionParameters;
                    dbEntry.UseNameAndPassword = configuration.UseNameAndPassword;
                    dbEntry.UseSecureAuthentication = configuration.UseSecureAuthentication;
                    dbEntry.UsedNameForEmailConnection = configuration.UsedNameForEmailConnection;
                }
            }
            _context.SaveChanges();
        }

        public MailServerConfiguration DeleteConfiguration(int Id)
        {
            MailServerConfiguration dbEntry = _context.MailServerConfigurations.FirstOrDefault(p => p.ConfigurationId == Id);
            if (dbEntry != null)
            {
                _context.MailServerConfigurations.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }

        #endregion
    }
}
