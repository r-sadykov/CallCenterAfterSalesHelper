using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class MailServerListViewComponent : ViewComponent
    {
        private readonly IMailServerConfiguration _repository;

        public MailServerListViewComponent(IMailServerConfiguration repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("MailServerList", _repository.MailServerConfigurations.OrderBy(x => x.ConfigurationName));
        }
    }
}