using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IDatabaseConfiguration
    {
        IQueryable<DatabaseConfiguration> DatabaseConfigurations { get; }

        void SaveConfiguration(DatabaseConfiguration databaseConfiguration);
        DatabaseConfiguration DeleteConfiguration(int configurationId);
    }
}
