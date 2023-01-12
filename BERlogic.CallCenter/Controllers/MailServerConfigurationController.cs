using System;
using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MimeKit;
using MimeKit.Utils;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class MailServerConfigurationController : Controller
    {
        private readonly IMailServerConfiguration _repository;
        private readonly IStringLocalizer<MailServerConfigurationController> _localizer;

        public MailServerConfigurationController(IMailServerConfiguration repository, IStringLocalizer<MailServerConfigurationController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult CreateMailServerConfiguration() => View("MailServerIndex");

        [HttpPost]
        public IActionResult CreateMailServerConfiguration(MailServerConfigurationViewModel configuration)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveConfiguration(configuration.MailServerConfiguration);
                TempData["message"] = $"{configuration.MailServerConfiguration.ConfigurationName} {_localizer["has been saved"]}";
                return RedirectToAction("CreateMailServerConfiguration");
            }
            TempData["testError"] = $"{configuration.MailServerConfiguration.ConfigurationName} {_localizer["has been not saved"]}";
            return View("MailServerIndex", configuration);
        }
        public IActionResult TestMailServerConfiguration(MailServerConfigurationViewModel configuration)
        {
            if (ModelState.IsValid)
            {
                if (configuration != null)
                {
                    try
                    {
                        using (var client = new SmtpClient())
                        {
                            client.Connect(configuration.MailServerConfiguration.ServerNameOfOutcomeMessages, configuration.MailServerConfiguration.ServerPortOfOutcomeMessages, (SecureSocketOptions)configuration.MailServerConfiguration.ServerSecureConnectionParameters);
                            client.Authenticate(configuration.MailServerConfiguration.Address, configuration.MailServerConfiguration.Password);
                            var mail = new MimeMessage();
                            mail.From.Add(new MailboxAddress(configuration.MailServerConfiguration.UserName, configuration.MailServerConfiguration.Address));
                            mail.To.Add(new MailboxAddress(configuration.EMail));
                            mail.MessageId = MimeUtils.GenerateMessageId();
                            mail.Date = DateTimeOffset.Now;
                            mail.Subject = $"{_localizer["Test Message from BERlogic CallCenter WebApp"]}";
                            var builder = new BodyBuilder();
                            builder.HtmlBody += $"<p><b><i>{_localizer["This is a test that client could receive mail from App"]}</i></b></p>";
                                builder.TextBody += $"{_localizer["This is a test that client could receive mail from App"]}";
                            mail.Body = builder.ToMessageBody();
                            client.Send(mail);
                        }
                        TempData["testConnection"] = $"{_localizer["Mail sent successful"]}";
                    }
                    catch (Exception e)
                    {
                        TempData["testError"] = $"{_localizer["Sending mail was failed.<br/>Error is"]} {e.Message}";
                    }
                }
               // return RedirectToAction("CreateMailServerConfiguration");
            }
            return View("MailServerIndex", configuration);
        }

        [HttpGet]
        public IActionResult EditMailServerConfiguration(int configurationId)
        {
            MailServerConfiguration configuration =
                _repository.MailServerConfigurations.FirstOrDefault(p => p.ConfigurationId == configurationId);
            return View("MailServerIndex", new MailServerConfigurationViewModel {MailServerConfiguration = configuration, EMail = String.Empty });
        } 

        [HttpPost]
        public IActionResult EditMailServerConfiguration(MailServerConfigurationViewModel configuration)
        {
            if (ModelState.IsValid)
            {
                _repository.SaveConfiguration(configuration.MailServerConfiguration);
                TempData["message"] = $"{configuration.MailServerConfiguration.ConfigurationName} {_localizer["has been changed"]}";
                return RedirectToAction("CreateMailServerConfiguration");
            }

            return View("MailServerIndex", configuration);
        }

        public IActionResult RemoveMailServerConfiguration(int configurationId)
        {
            MailServerConfiguration deletedConfiguration = _repository.DeleteConfiguration(configurationId);
            if (deletedConfiguration != null)
            {
                TempData["message"] = $"{deletedConfiguration.ConfigurationName} {_localizer["was deleted"]}";
            }

            return RedirectToAction("CreateMailServerConfiguration");
        }
    }
}