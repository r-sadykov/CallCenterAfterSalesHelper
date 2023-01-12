using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IFileReportJournal
    {
        IQueryable<FullReportJournal> Journals { get; }
        void Save(FullReportJournal journal);
    }
}
