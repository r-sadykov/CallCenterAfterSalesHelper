using System.Linq;

namespace BERlogic.CallCenter.Configuration
{
    public class UserRepository : ISettings
    {
        public UserRepository(IQueryable<UserSettings> settingses)
        {
            Settingses = settingses;
        }

        public IQueryable<UserSettings> Settingses { get; }
    }
}
