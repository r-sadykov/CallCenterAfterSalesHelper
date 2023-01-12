using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class MailFilterController : Controller
    {
        private readonly IMailFilter _repository;
        private readonly IStringLocalizer<MailFilterController> _localizer;

        public MailFilterController(IMailFilter repository, IStringLocalizer<MailFilterController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult CreateFilter() => View("MailFilterIndex");

        [HttpPost]
        public IActionResult CreateFilter(MailFilter filter)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveFilter(filter);
                TempData["message"] = $"{filter.MailThemes} {_localizer["for"]} {filter.MailAddress} {_localizer["has been saved"]}";
                return RedirectToAction("CreateFilter");
            }
            TempData["testError"] = $"{filter.MailThemes} {_localizer["for"]} {filter.MailAddress} {_localizer["has been not saved"]}";
            return View("MailFilterIndex", filter);
        }

        [HttpGet]
        public IActionResult EditFilter(int filterId) => View("MailFilterIndex", _repository.MailFilters.FirstOrDefault(p => p.MailFiltersId == filterId));

        [HttpPost]
        public IActionResult EditFilter(MailFilter filter)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveFilter(filter);
                TempData["message"] = $"Data for {filter.MailAddress} {_localizer["has been changed"]}";
                return RedirectToAction("CreateFilter");
            }

            return View("MailFilterIndex", filter);
        }

        public IActionResult RemoveFilter(int filterId)
        {
            MailFilter deletedFilter = _repository.DeleteFilter(filterId);
            if (deletedFilter != null)
            {
                TempData["message"] = $"{deletedFilter.MailThemes} {_localizer["for"]} {deletedFilter.MailAddress} {_localizer["was deleted"]}";
            }

            return RedirectToAction("CreateFilter");
        }
    }
}