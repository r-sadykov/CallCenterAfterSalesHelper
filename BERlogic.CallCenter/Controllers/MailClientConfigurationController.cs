using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class MailClientConfigurationController : Controller
    {
        private readonly IMailClientConfiguration _repository;
        private readonly IStringLocalizer<MailClientConfigurationController> _localizer;

        public MailClientConfigurationController(IMailClientConfiguration repository, IStringLocalizer<MailClientConfigurationController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult CreateMailClientConfiguration() => View("MailClientIndex");

        [HttpPost]
        public IActionResult CreateMailClientConfiguration(MailClientConfiguration configuration)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveConfiguration(configuration);
                TempData["message"] = $"{configuration.ConfigurationName} {_localizer["has been saved"]}";
                return RedirectToAction("CreateMailClientConfiguration");
            }
            TempData["testError"] = $"{configuration.ConfigurationName} {_localizer["has been not saved"]}";
            return View("MailClientIndex", configuration);
        }

        [HttpGet]
        public IActionResult EditMailClientConfiguration(int configurationId) => View("MailClientIndex", _repository.MailClientConfigurations.FirstOrDefault(p => p.ConfigurationId == configurationId));

        [HttpPost]
        public IActionResult EditMailClientConfiguration(MailClientConfiguration configuration)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveConfiguration(configuration);
                TempData["message"] = $"{configuration.ConfigurationName} {_localizer["has been changed"]}";
                return RedirectToAction("CreateMailClientConfiguration");
            }

            return View("MailClientIndex", configuration);
        }

        public IActionResult RemoveMailClientConfiguration(int configurationId)
        {
            MailClientConfiguration deletedConfiguration = _repository.DeleteConfiguration(configurationId);
            if (deletedConfiguration != null)
            {
                TempData["message"] = $"{deletedConfiguration.ConfigurationName} {_localizer["was deleted"]}";
            }

            return RedirectToAction("CreateMailClientConfiguration");
        }
    }
}