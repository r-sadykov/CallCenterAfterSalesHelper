using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class MailFilterListViewComponent : ViewComponent
    {
        private readonly IMailFilter _repository;

        public MailFilterListViewComponent(IMailFilter repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("MailFilterList", _repository.MailFilters.OrderBy(x => x.MailAddress));
        }
    }
}