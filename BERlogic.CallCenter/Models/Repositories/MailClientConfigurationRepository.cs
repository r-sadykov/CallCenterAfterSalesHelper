using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class MailClientConfigurationRepository:IMailClientConfiguration
    {
        private readonly ApplicationDbContext _context;

        public MailClientConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<MailClientConfiguration> MailClientConfigurations => _context.MailClientConfigurations;

        public void SaveConfiguration(MailClientConfiguration configuration)
        {
            if (configuration.ConfigurationId == 0)
            {
                _context.MailClientConfigurations.Add(configuration);
            }
            else
            {
                MailClientConfiguration dbEntry = _context.MailClientConfigurations.FirstOrDefault(p => p.ConfigurationId == configuration.ConfigurationId);
                if (dbEntry != null)
                {
                    dbEntry.ConfigurationName = configuration.ConfigurationName;
                    dbEntry.SourceFolder = configuration.SourceFolder;
                    dbEntry.TargetFolder = configuration.TargetFolder;
                }
            }
            _context.SaveChanges();
        }

        public MailClientConfiguration DeleteConfiguration(int Id)
        {
            MailClientConfiguration dbEntry = _context.MailClientConfigurations.FirstOrDefault(p => p.ConfigurationId == Id);
            if (dbEntry != null)
            {
                _context.MailClientConfigurations.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
