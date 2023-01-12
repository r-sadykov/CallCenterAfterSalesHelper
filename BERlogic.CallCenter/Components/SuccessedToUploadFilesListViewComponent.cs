using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class SuccessedToUploadFilesListViewComponent : ViewComponent
    {
        private readonly IFileInFolder _repository;

        public SuccessedToUploadFilesListViewComponent(IFileInFolder repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("SuccessToUploadFileList", _repository.FilesInSuccessFolder.OrderByDescending(x => x.DateTimeOfUpload).Take(5));
        }
    }
}