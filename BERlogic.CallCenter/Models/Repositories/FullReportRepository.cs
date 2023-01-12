using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class FullReportRepository:IFullReport
    {
        private readonly ApplicationDbProcessContext context;

        public FullReportRepository(ApplicationDbProcessContext _context)
        {
            context = _context;
        }

        public IQueryable<FullReport> FullReports => context.FullReports;
    }
}
