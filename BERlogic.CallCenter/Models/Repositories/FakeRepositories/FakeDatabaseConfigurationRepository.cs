using System.Collections.Generic;
using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories.FakeRepositories
{
    public class FakeDatabaseConfigurationRepository:IDatabaseConfiguration
    {
        public IQueryable<DatabaseConfiguration> DatabaseConfigurations=>new List<DatabaseConfiguration>
        {
            new DatabaseConfiguration{ConfigurationId = 1, ConfigurationName = "DB Test 1",
                DataSource = "test data source", UserName = "golden boy", Password = "golden",
                InitialCatalog = "mama_rosa", IntegratedSecurity = false, LocalPathString = @"D:\TestFolder\Test1.mdf"},
            new DatabaseConfiguration{ConfigurationId = 2, ConfigurationName = "DB Test 2",
                DataSource = "test data source 2", UserName = "silver boy", Password = "silver",
                InitialCatalog = "mama_mia", IntegratedSecurity = true, LocalPathString = @"D:\TestFolder\Test2.mdf"}
        }.AsQueryable();

        public void SaveConfiguration(DatabaseConfiguration databaseConfiguration)
        {
            if (databaseConfiguration.ConfigurationId==0)
            {
                DatabaseConfigurations.ToList().Add(databaseConfiguration);
            }
            else
            {
                DatabaseConfiguration configuration =
                    DatabaseConfigurations.FirstOrDefault(p => p.ConfigurationId ==
                                                               databaseConfiguration.ConfigurationId);
                if (configuration!=null)
                {
                    configuration.ConfigurationName = databaseConfiguration.ConfigurationName;
                    configuration.DataSource = databaseConfiguration.DataSource;
                    configuration.InitialCatalog = databaseConfiguration.InitialCatalog;
                    configuration.IntegratedSecurity = databaseConfiguration.IntegratedSecurity;
                    configuration.LocalPathString = databaseConfiguration.LocalPathString;
                    configuration.UserName = databaseConfiguration.UserName;
                    configuration.Password = databaseConfiguration.Password;
                }
            }
        }

        public DatabaseConfiguration DeleteConfiguration(int configurationId)
        {
            DatabaseConfiguration configuration = DatabaseConfigurations.FirstOrDefault(p=>p.ConfigurationId==configurationId);
            if (configuration!=null)
            {
                DatabaseConfigurations.ToList().Remove(configuration);
            }

            return configuration;
        }
    }
}
