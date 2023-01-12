using System.Linq;

namespace BERlogic.CallCenter.Configuration
{
    public interface ISettings
    {
        IQueryable<UserSettings> Settingses { get; }
    }
}
