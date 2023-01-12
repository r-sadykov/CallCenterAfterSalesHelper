using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IFullReport
    {
        IQueryable<FullReport> FullReports { get; }
    }
}
