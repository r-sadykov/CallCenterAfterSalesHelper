using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class DatabaseConfigurationRepository:IDatabaseConfiguration
    {
        private readonly ApplicationDbContext _context;

        public DatabaseConfigurationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #region Implementation of IDatabaseConfiguration

        public IQueryable<DatabaseConfiguration> DatabaseConfigurations => _context.DatabaseConfigurations;

        public void SaveConfiguration(DatabaseConfiguration databaseConfiguration)
        {
            if (databaseConfiguration.ConfigurationId==0)
            {
                _context.DatabaseConfigurations.Add(databaseConfiguration);
            }
            else
            {
                DatabaseConfiguration dbEntry = _context.DatabaseConfigurations.FirstOrDefault(p=>p.ConfigurationId==databaseConfiguration.ConfigurationId);
                if (dbEntry!=null)
                {
                    dbEntry.ConfigurationName = databaseConfiguration.ConfigurationName;
                    dbEntry.DataSource = databaseConfiguration.DataSource;
                    dbEntry.InitialCatalog = databaseConfiguration.InitialCatalog;
                    dbEntry.IntegratedSecurity = databaseConfiguration.IntegratedSecurity;
                    dbEntry.LocalPathString = databaseConfiguration.LocalPathString;
                    dbEntry.UserName = databaseConfiguration.UserName;
                    dbEntry.Password = databaseConfiguration.Password;
                }
            }

            _context.SaveChanges();
        }

        public DatabaseConfiguration DeleteConfiguration(int configurationId)
        {
            DatabaseConfiguration dbEntry = _context.DatabaseConfigurations.FirstOrDefault(p=>p.ConfigurationId==configurationId);
            if (dbEntry != null)
            {
                _context.DatabaseConfigurations.Remove(dbEntry);
                _context.SaveChanges();
            }

            return dbEntry;
        }

        #endregion
    }
}
