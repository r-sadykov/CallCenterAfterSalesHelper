using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class DatabaseLastPnrAfterUploadListViewComponent : ViewComponent
    {
        private readonly IFileReportJournal _repository;

        public DatabaseLastPnrAfterUploadListViewComponent(IFileReportJournal repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("PnrLastSuccessEntryList", _repository.Journals.OrderByDescending(x => x.RegistrationEventDate).Take(10));
        }
    }
}