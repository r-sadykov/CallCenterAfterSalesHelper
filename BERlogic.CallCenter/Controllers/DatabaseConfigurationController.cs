using System;
using System.Data.SqlClient;
using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class DatabaseConfigurationController : Controller
    {
        private readonly IDatabaseConfiguration _repository;
        private readonly IStringLocalizer<DatabaseConfigurationController> _localizer;

        public DatabaseConfigurationController(IDatabaseConfiguration repository, IStringLocalizer<DatabaseConfigurationController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult CreateDatabaseConfiguration() => View("DatabaseIndex");

        [HttpPost]
        public IActionResult CreateDatabaseConfiguration(DatabaseConfiguration configuration)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveConfiguration(configuration);
                TempData["message"] = $"{configuration.ConfigurationName} {_localizer["has been saved"]}";
                return RedirectToAction("CreateDatabaseConfiguration");
            }
            TempData["testError"] = $"{configuration.ConfigurationName} {_localizer["has been not saved"]}";
            return View("DatabaseIndex", configuration);
        }

        public IActionResult TestDatabaseConfiguration(int configurationId)
        {
            DatabaseConfiguration configuration = _repository.DatabaseConfigurations.FirstOrDefault(p=>p.ConfigurationId==configurationId);
            if (configuration != null)
            {
                using (var connection = new SqlConnection(configuration.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        TempData["testConnection"] = $"{_localizer["Connection opened successful"]}";
                    }
                    catch (Exception e)
                    {
                        TempData["testError"] = $"{_localizer["Connection failed.<br/>Error is"]} {e.Message}";
                    }
                }
            }
            return RedirectToAction("CreateDatabaseConfiguration");
        }
        [HttpGet]
        public IActionResult EditDatabaseConfiguration(int configurationId) => View("DatabaseIndex",_repository.DatabaseConfigurations.FirstOrDefault(p=>p.ConfigurationId==configurationId));

        [HttpPost]
        public IActionResult EditDatabaseConfiguration(DatabaseConfiguration configuration)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveConfiguration(configuration);
                TempData["message"] = $"{configuration.ConfigurationName} {_localizer["has been changed"]}";
                return RedirectToAction("CreateDatabaseConfiguration");
            }

            return View("DatabaseIndex",configuration);
        }

        public IActionResult RemoveDatabaseConfiguration(int configurationId)
        {
            DatabaseConfiguration deletedConfiguration = _repository.DeleteConfiguration(configurationId);
            if (deletedConfiguration!=null)
            {
                TempData["message"] = $"{deletedConfiguration.ConfigurationName} {_localizer["was deleted"]}";
            }

            return RedirectToAction("CreateDatabaseConfiguration");
        }
    }
}