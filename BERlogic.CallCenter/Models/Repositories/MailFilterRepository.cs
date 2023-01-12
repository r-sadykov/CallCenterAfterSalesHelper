using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class MailFilterRepository:IMailFilter
    {
        private readonly ApplicationDbProcessContext _context;

        public MailFilterRepository(ApplicationDbProcessContext context)
        {
            _context = context;
        }

        public IQueryable<MailFilter> MailFilters => _context.MailFilters;
        public void SaveFilter(MailFilter filter)
        {
            if (filter.MailFiltersId == 0)
            {
                _context.MailFilters.Add(filter);
            }
            else
            {
                MailFilter dbEntry = _context.MailFilters.FirstOrDefault(p => p.MailFiltersId == filter.MailFiltersId);
                if (dbEntry != null)
                {
                    dbEntry.MailAddress = filter.MailAddress;
                    dbEntry.MailThemes = filter.MailThemes;
                }
            }
            _context.SaveChanges();
        }

        public MailFilter DeleteFilter(int Id)
        {
            MailFilter dbEntry = _context.MailFilters.FirstOrDefault(p => p.MailFiltersId == Id);
            if (dbEntry != null)
            {
                _context.MailFilters.Remove(dbEntry);
                _context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
