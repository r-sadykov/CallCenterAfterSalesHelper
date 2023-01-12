using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Enums;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.Services;
using BERlogic.CallCenter.Templates;
using BERlogic.CallCenter.ViewModels;
using DinkToPdf.Contracts;
using DinkToPdf.Documents;
using DinkToPdf.Settings;
using DinkToPdf.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class PassengerRebookingController : Controller
    {
        private readonly IFullReport _repository;
        private readonly IServiceOperations _operations;
        private readonly ITempedServiceOperations _temped;
        private readonly IConverter _converter;
        private readonly IMailServerConfiguration _mailServer;
        private readonly IEmailSender _mailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IStringLocalizer<PassengerRebookingController> _localizer;

        public PassengerRebookingController(IFullReport repository, IServiceOperations operations, ITempedServiceOperations temped, IConverter converter, IMailServerConfiguration mailServer, IEmailSender mailSender, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, IStringLocalizer<PassengerRebookingController> localizer)
        {
            _repository = repository;
            _operations = operations;
            _temped = temped;
            _converter = converter;
            _mailServer = mailServer;
            _mailSender = mailSender;
            _userManager = userManager;
            _signInManager = signInManager;
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Index()
        {
            TempData["message"] = null;
            TempData["error"] = null;
            GetPassengerDataViewModel objModel = new GetPassengerDataViewModel(_localizer);
            return View(objModel);
        }

        [HttpPost]
        public IActionResult Index(GetPassengerDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                IQueryable<FullReport> reports = null;
                switch (model.SelectedItem)
                {
                    case "by PNR":
                        reports = _repository.FullReports.Where(p => p.FirstGdsBookingNumber == model.InputField);
                        break;
                    case "by Booking Number":
                        int.TryParse(model.InputField, out var number);
                        reports = _repository.FullReports.Where(p => p.BookingNumber == number);
                        break;
                }

                if (reports != null && reports.ToList().Any())
                {
                    TempData["message"] = $"{_localizer["Found"]} {reports.Count()} {_localizer["objects"]}";
                    model.PassengersData = reports;
                }
                else
                {
                    TempData["testError"] = _localizer["Do not found any entry!"];
                }
            }

            model.Localizer = _localizer;
            return View(model);
        }
        [HttpGet]
        public IActionResult Rebooking(int id = 0, int viewModelId = 0)
        {
            if (viewModelId == 0 && id != 0)
            {
                var report = _repository.FullReports.FirstOrDefault(p => p.Id == id);
                if (report != null)
                {
                    ServiceOperationsModel smodel = new ServiceOperationsModel
                    {
                        AgencyCode = report.SystemAgency,
                        AgentName = report.Agent,
                        AdditionalServicesAndBaggage = 0M,
                        AirlineTotalPrice = (report.Tax + report.Tariff),
                        BookingDate = report.BookingDate,
                        BookingNumber = report.BookingNumber,
                        BookingCode = report.FirstGdsBookingNumber,
                        ChangeServiceDate = DateTime.Now,
                        CustomerFullName = report.CustomerFullName,
                        CustomerEmail = report.CustomerEmail,
                        PaymentMethod = report.PaymentMethod,
                        ClearingType = report.ClearingType,
                        Salespoint = report.SalesPoint,
                        PaymentLink = string.Empty,
                        PriceDifference = report.Tariff,
                        RebookingFee = report.Tax,
                        BerlogicFee = 20M,
                        ServiceOperations = ServiceOperations.Rebooking,
                        NonRefundableTaxes = report.Tax,
                        CancellationAirlineFee = report.Tariff,
                        PrepaidBerlogicFee = 0M
                    };
                    List<LineRoute> routesList = new List<LineRoute>();
                    var routes = report.FirstRoute.Split(',');
                    for (var index = 0; index < routes.Length; index++)
                    {
                        var t = routes[index];
                        LineRoute route = new LineRoute
                        {
                            DepartureAirport = t.Split('-')[0].Trim(),
                            ArrivalAirport = t.Split('-')[1].Trim(),
                            Baggage = string.Empty,
                            FlightAirline = report.FirstAirline.Trim(),
                            DepartureTime = DateTime.Now,
                            ArrivalTime = DateTime.Now,
                            FlightNumber = 0
                        };
                        if (index == 0)
                        {
                            route.DepartureDate = report.DepartureDate;
                            route.ArrivalDate = report.DepartureDate;
                        }
                        else if (index == routes.Length - 1)
                        {
                            route.DepartureDate = report.ReturnDate;
                            route.ArrivalDate = report.ReturnDate;
                        }
                        else
                        {
                            route.DepartureDate = DateTime.Now;
                            route.ArrivalDate = DateTime.Now;
                        }
                        routesList.Add(route);
                    }
                    routesList.Add(new LineRoute());
                    smodel.LineRoutes = routesList;
                    List<PassangerModel> passangerList = new List<PassangerModel>();
                    var passangers = report.PassengerNames.Split(',');
                    foreach (var s in passangers)
                    {
                        PassangerModel passanger = new PassangerModel();
                        var temp = s.Split(' ');
                        passanger.Prefix = temp[0].Trim();
                        passanger.FirstName = temp[1].Trim();
                        passanger.LastName = temp[temp.Length - 1].Trim();
                        for (int i = 2; i < temp.Length - 1; i++)
                        {
                            passanger.MiddleName += temp[i].Trim() + " ";
                        }
                        passanger.PassangerFee = 0M;
                        passangerList.Add(passanger);
                    }
                    smodel.PassangerModels = passangerList;
                    _temped.CleanRepository();
                    ServiceOperationsViewModel viewModel = new ServiceOperationsViewModel
                    {
                        AgencyCode = smodel.AgencyCode,
                        AgentName = smodel.AgentName,
                        AdditionalServicesAndBaggage = smodel.AdditionalServicesAndBaggage.ToString("##.00"),
                        AirlineTotalPrice = smodel.AirlineTotalPrice.ToString("##.00"),
                        BookingDate = smodel.BookingDate,
                        BookingNumber = smodel.BookingNumber,
                        BookingCode = smodel.BookingCode,
                        ChangeServiceDate = smodel.ChangeServiceDate,
                        CustomerFullName = smodel.CustomerFullName,
                        CustomerEmail = smodel.CustomerEmail,
                        PaymentMethod = smodel.PaymentMethod,
                        ClearingType = smodel.ClearingType,
                        Salespoint = smodel.Salespoint,
                        PaymentLink = smodel.PaymentLink,
                        PriceDifference = smodel.PriceDifference.ToString("##.00"),
                        RebookingFee = smodel.RebookingFee.ToString("##.00"),
                        BerlogicFee = smodel.BerlogicFee.ToString("##.00"),
                        ServiceOperations = smodel.ServiceOperations,
                        NonRefundableTaxes = smodel.NonRefundableTaxes.ToString("##.00"),
                        CancellationAirlineFee = smodel.CancellationAirlineFee.ToString("##.00"),
                        PrepaidBerlogicFee = smodel.PrepaidBerlogicFee.ToString("##.00"),
                        LineRoutes = smodel.LineRoutes
                    };
                    viewModel.PassangerModels = new List<PassangerModelViewModel>();
                    foreach (var passangerModel in smodel.PassangerModels)
                    {
                        PassangerModelViewModel passangerModelViewModel =
                            new PassangerModelViewModel
                            {
                                Id = passangerModel.Id,
                                FirstName = passangerModel.FirstName,
                                LastName = passangerModel.LastName,
                                MiddleName = passangerModel.MiddleName,
                                Prefix = passangerModel.Prefix,
                                PassangerFee = passangerModel.PassangerFee.ToString("##.00")
                            };
                        viewModel.PassangerModels.Add(passangerModelViewModel);
                    }
                    _temped.Add(viewModel);
                    return View("Rebooking", viewModel);
                }
            }
            return View("Rebooking", _temped.TempedOperationsModels.LastOrDefault());
        }

        [HttpPost]
        public IActionResult Rebooking(ServiceOperationsViewModel model)
        {
            if (ModelState.IsValid)
            {
                for (int i = model.LineRoutes.Count - 1; i >= 0; i--)
                {
                    if (String.IsNullOrWhiteSpace(model.LineRoutes[i].DepartureAirport) && String.IsNullOrWhiteSpace(model.LineRoutes[i].ArrivalAirport))
                    {
                        model.LineRoutes.RemoveAt(i);
                    }
                }

                /*decimal.TryParse(model.AdditionalServicesAndBaggage, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal additionalServiceAndBaggage);*/

                var temp = model.AdditionalServicesAndBaggage;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal additionalServiceAndBaggage);

                temp = model.AirlineTotalPrice;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal airlineTotalPrice);

                temp = model.PriceDifference;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal priceDifference);

                temp = model.RebookingFee;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal rebookingFee);

                temp = model.BerlogicFee;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal berlogicFee);

                temp = model.NonRefundableTaxes;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal nonRefundableTaxes);

                temp = model.CancellationAirlineFee;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal cancellationAirlineFee);

                temp = model.PrepaidBerlogicFee;
                temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal prepaidBerlogicFee);

                List<PassangerModel> passangerModels = new List<PassangerModel>();
                foreach (var passangerModel in model.PassangerModels)
                {
                    PassangerModel passanger = new PassangerModel
                    {
                        FirstName = passangerModel.FirstName,
                        LastName = passangerModel.LastName,
                        MiddleName = passangerModel.MiddleName,
                        Prefix = passangerModel.Prefix
                    };

                    temp = passangerModel.PassangerFee;
                    temp = string.IsNullOrWhiteSpace(temp) ? "0.0" : temp.Replace(',', '.');

                    decimal.TryParse(temp, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal passangerFee);
                    passanger.PassangerFee = passangerFee;
                    passangerModels.Add(passanger);
                }

                ServiceOperationsModel serviceOperation = new ServiceOperationsModel
                {
                    AgencyCode = model.AgencyCode,
                    AgentName = model.AgentName,
                    AdditionalServicesAndBaggage = additionalServiceAndBaggage,
                    AirlineTotalPrice = airlineTotalPrice,
                    BookingDate = model.BookingDate,
                    BookingNumber = model.BookingNumber,
                    BookingCode = model.BookingCode,
                    ChangeServiceDate = model.ChangeServiceDate,
                    CustomerFullName = model.CustomerFullName,
                    CustomerEmail = model.CustomerEmail,
                    PaymentMethod = model.PaymentMethod,
                    ClearingType = model.ClearingType,
                    Salespoint = model.Salespoint,
                    PaymentLink = model.PaymentLink,
                    PriceDifference = priceDifference,
                    RebookingFee = rebookingFee,
                    BerlogicFee = berlogicFee * model.PassangerModels.Count,
                    ServiceOperations = model.ServiceOperations,
                    NonRefundableTaxes = nonRefundableTaxes,
                    CancellationAirlineFee = cancellationAirlineFee,
                    PrepaidBerlogicFee = prepaidBerlogicFee,
                    LineRoutes = model.LineRoutes,
                    PassangerModels = passangerModels
                };
                int id = 0;
                if (model.ViewModelId == 0)
                {
                    id = _operations.Save(serviceOperation);
                }
                else
                {
                    serviceOperation.Id = model.ViewModelId;
                    id = model.ViewModelId;
                    _operations.Save(serviceOperation);
                }
                TempData["message"] = _localizer["Information saved"];
                var operation = _operations.ServiceOperationsModels.FirstOrDefault(p => p.Id == id);
                OperationsServiceViewModel viewModel = new OperationsServiceViewModel
                {
                    MailServer = _mailServer.MailServerConfigurations,
                    ServiceOperations = operation
                };
                return View("RebookingSuccess", viewModel);
            }

            TempData["error"] = _localizer["Information failed to save"];
            return View("Rebooking", model);
        }

        [HttpPost]
        public IActionResult RemoveRoute(int routeId, ServiceOperationsViewModel model)
        {
            var route = model.LineRoutes[routeId];
            model.LineRoutes.Remove(route);
            _temped.CleanRepository();
            _temped.Add(model);
            return RedirectToAction("Rebooking", _temped.TempedOperationsModels.LastOrDefault()?.ViewModelId);
        }

        [HttpPost]
        public IActionResult AddRoute(ServiceOperationsViewModel model)
        {
            model.LineRoutes.Add(new LineRoute());
            _temped.CleanRepository();
            _temped.Add(model);
            return RedirectToAction("Rebooking", _temped.TempedOperationsModels.LastOrDefault()?.ViewModelId);
        }

        [HttpPost]
        public IActionResult RemovePassanger(int pnrId, ServiceOperationsViewModel model)
        {
            var passanger = model.PassangerModels[pnrId];
            model.PassangerModels.Remove(passanger);
            _temped.CleanRepository();
            _temped.Add(model);
            return RedirectToAction("Rebooking", _temped.TempedOperationsModels.LastOrDefault()?.ViewModelId);
        }

        [HttpGet]
        public IActionResult EditRebookings(int id)
        {
            var model = _operations.ServiceOperationsModels.Include(p => p.LineRoutes).Include(p => p.PassangerModels).Where(p => p.Id == id).ToList().FirstOrDefault();


            ServiceOperationsViewModel viewModel = new ServiceOperationsViewModel
            {
                ViewModelId = model.Id,
                AgencyCode = model.AgencyCode,
                AgentName = model.AgentName,
                AdditionalServicesAndBaggage = model.AdditionalServicesAndBaggage.ToString("##.00"),
                AirlineTotalPrice = model.AirlineTotalPrice.ToString("##.00"),
                BookingDate = model.BookingDate,
                BookingNumber = model.BookingNumber,
                BookingCode = model.BookingCode,
                ChangeServiceDate = model.ChangeServiceDate,
                CustomerFullName = model.CustomerFullName,
                CustomerEmail = model.CustomerEmail,
                PaymentMethod = model.PaymentMethod,
                ClearingType = model.ClearingType,
                Salespoint = model.Salespoint,
                PaymentLink = model.PaymentLink,
                PriceDifference = model.PriceDifference.ToString("##.00"),
                RebookingFee = model.RebookingFee.ToString("##.00"),
                BerlogicFee = (model.BerlogicFee / model.PassangerModels.Count).ToString("##.00"),
                ServiceOperations = model.ServiceOperations,
                NonRefundableTaxes = model.NonRefundableTaxes.ToString("##.00"),
                CancellationAirlineFee = model.CancellationAirlineFee.ToString("##.00"),
                PrepaidBerlogicFee = model.PrepaidBerlogicFee.ToString("##.00"),
                LineRoutes = model.LineRoutes
            };
            viewModel.PassangerModels = new List<PassangerModelViewModel>();
            foreach (var passangerModel in model.PassangerModels)
            {
                PassangerModelViewModel passangerModelViewModel =
                    new PassangerModelViewModel
                    {
                        Id = passangerModel.Id,
                        FirstName = passangerModel.FirstName,
                        LastName = passangerModel.LastName,
                        MiddleName = passangerModel.MiddleName,
                        Prefix = passangerModel.Prefix,
                        PassangerFee = passangerModel.PassangerFee.ToString("##.00")
                    };
                viewModel.PassangerModels.Add(passangerModelViewModel);
            }
            return View("Rebooking", viewModel);
        }

        [HttpGet]
        public IActionResult RebookingSuccess(int id)
        {
            var model = _operations.ServiceOperationsModels.Include(p => p.LineRoutes).Include(p => p.PassangerModels).Where(p => p.Id == id).ToList().FirstOrDefault();
            OperationsServiceViewModel viewModel = new OperationsServiceViewModel
            {
                MailServer = _mailServer.MailServerConfigurations,
                ServiceOperations = model
            };
            return View("RebookingSuccess", viewModel);
        }

        [HttpGet]
        public IActionResult SavePdf(int id)
        {
            var model = _operations.ServiceOperationsModels.Include(p => p.LineRoutes).Include(p => p.PassangerModels).Where(p => p.Id == id).ToList().FirstOrDefault();
            if (model != null && model.Id != 0)
            {
                string title = $"Rebooking Report _ {model.CustomerFullName}_{DateTime.Now:yy-MM-dd HH-mm}";
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo_for_PDF.png");
                string htmlContent = TemplateGenerator.GetHtmlRebookingPdfString(model, logoPath);
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "bootstrap.css");
                string footerText = "Commercial Registration: Berlin Charlottenburg HRB 0001 - Managing Director: Abra Kadabra";
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings {Top = 10, Bottom = 10, Left = 10, Right = 10},
                        DocumentTitle = title
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            PagesCount = true,
                            HtmlContent = htmlContent,
                            WebSettings =
                            {
                                DefaultEncoding = "utf-8",
                                UserStyleSheet = cssPath
                            },
                            HeaderSettings =
                            {
                                FontName = "Arial",
                                FontSize = 9,
                                Right = "Page [page] of [toPage]",
                                Line = true
                            },
                            FooterSettings = {FontName = "Arial", FontSize = 9, Line = true, Center = footerText}
                        }
                    }
                };
                var file = _converter.Convert(doc);
                return File(file, "application/pdf", title);
            }

            return RedirectToAction("RebookingSuccess", id);
        }

        [HttpPost]
        public IActionResult SendRebookingMail(int id, OperationsServiceViewModel viewModel)
        {
            var model = _operations.ServiceOperationsModels.Include(p => p.LineRoutes).Include(p => p.PassangerModels)
                                   .Where(p => p.Id == id).ToList().FirstOrDefault();
            var mailConfig =
                _mailServer.MailServerConfigurations.FirstOrDefault(p => p.ConfigurationId == viewModel.SelectedItem);
            if (model != null && model.Id != 0 && viewModel.SelectedItem != 0)
            {
                string title = $"Rebooking Report _ {model.CustomerFullName}_{DateTime.Now:yy-MM-dd HH-mm}";
                string logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "logo_for_PDF.png");
                string htmlContent = TemplateGenerator.GetHtmlRebookingPdfString(model, logoPath);
                string cssPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "bootstrap.css");
                string footerText = "Commercial Registration: Berlin Charlottenburg HRB 0001 - Managing Director: Abra Kadabra";
                var doc = new HtmlToPdfDocument
                {
                    GlobalSettings =
                    {
                        ColorMode = ColorMode.Color,
                        Orientation = Orientation.Portrait,
                        PaperSize = PaperKind.A4,
                        Margins = new MarginSettings {Top = 10, Bottom = 10, Left = 10, Right = 10},
                        DocumentTitle = title
                    },
                    Objects =
                    {
                        new ObjectSettings
                        {
                            PagesCount = true,
                            HtmlContent = htmlContent,
                            WebSettings =
                            {
                                DefaultEncoding = "utf-8",
                                UserStyleSheet = cssPath
                            },
                            HeaderSettings =
                            {
                                FontName = "Arial",
                                FontSize = 9,
                                Right = "Page [page] of [toPage]",
                                Line = true
                            },
                            FooterSettings = {FontName = "Arial", FontSize = 9, Line = true, Center = footerText}
                        }
                    }
                };
                var file = _converter.Convert(doc);
                var content = TemplateGenerator.GetHtmlRebookingMailString(model) +
                              TemplateGenerator.GetHtmlSignatureMailString(_userManager.GetUserAsync(HttpContext.User).Result);
                if (_mailSender.SendEmailAsync(model.CustomerEmail, "Umbuchungsbestätigung", content, mailConfig, file, title).Result)
                {
                    TempData["message"] = _localizer["EMail to customer were sent successful"];
                }
                else
                {
                    TempData["error"] = _localizer["EMail to customer were not sended"];
                }
            }
            viewModel = new OperationsServiceViewModel
            {
                MailServer = _mailServer.MailServerConfigurations,
                ServiceOperations = model
            };
            return View("RebookingSuccess", viewModel);
        }
    }
}