using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class FailedToUploadFilesListViewComponent : ViewComponent
    {
        private readonly IFileInFolder _repository;

        public FailedToUploadFilesListViewComponent(IFileInFolder repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("FailedToUploadFileList", _repository.FilesInFolder.OrderBy(x=>x.DateTimeOfUpload));
        }
    }
}