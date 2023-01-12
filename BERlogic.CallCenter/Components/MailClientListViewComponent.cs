using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class MailClientListViewComponent : ViewComponent
    {
        private readonly IMailClientConfiguration _repository;

        public MailClientListViewComponent(IMailClientConfiguration repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("MailClientList", _repository.MailClientConfigurations.OrderBy(x => x.ConfigurationName));
        }
    }
}