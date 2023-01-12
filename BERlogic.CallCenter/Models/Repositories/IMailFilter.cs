using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IMailFilter
    {
        IQueryable<MailFilter> MailFilters { get; }
        void SaveFilter(MailFilter filter);
        MailFilter DeleteFilter(int Id);
    }
}
