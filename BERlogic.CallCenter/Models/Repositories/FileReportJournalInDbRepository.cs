using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class FileReportJournalInDbRepository : IFileReportJournal
    {
        private readonly ApplicationDbProcessContext _context;

        public FileReportJournalInDbRepository(ApplicationDbProcessContext context)
        {
            _context = context;
        }
        #region Implementation of IFileReportJournal

        public IQueryable<FullReportJournal> Journals => _context.FullReportJournals;
        public void Save(FullReportJournal journal)
        {
            _context.FullReportJournals.Add(journal);
            _context.SaveChanges();
        }

        #endregion
    }
}
