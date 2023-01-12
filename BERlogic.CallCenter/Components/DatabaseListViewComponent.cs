using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class DatabaseListViewComponent:ViewComponent
    {
        private readonly IDatabaseConfiguration _repository;

        public DatabaseListViewComponent(IDatabaseConfiguration repository)
        {
            _repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("DatabaseList",_repository.DatabaseConfigurations.OrderBy(x=>x.ConfigurationName));
        }
    }
}
