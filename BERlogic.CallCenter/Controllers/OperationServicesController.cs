using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Enums;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.Services;
using BERlogic.CallCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Primitives;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class OperationServicesController : Controller
    {
        private readonly IServiceOperations _repository;
        private readonly IStringLocalizer<OperationServicesController> _localizer;

        public OperationServicesController(IServiceOperations repository, IStringLocalizer<OperationServicesController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }

        public IActionResult OperationsList(OperationServicesListViewModel viewModel, int productPage = 1)
        {
            var model = viewModel ?? new OperationServicesListViewModel();


            var repo = _repository.ServiceOperationsModels.Include(p => p.LineRoutes)
                .Include(p => p.PassangerModels).Where(p => p.ChangeServiceDate >= model.StartSearchDate && p.ChangeServiceDate <= model.EndSearchDate);
            if (model.Operations != ServiceOperations.All)
            {
                repo = repo.Where(p => p.ServiceOperations == model.Operations);
            }
            model.Services = repo.Skip((productPage - 1) * model.SelectedPageSize).Take(model.SelectedPageSize);
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = model.SelectedPageSize,
                TotalItems = repo.Count()
            };
            return View("OperationsList", model);

        }

        // ReSharper disable once InconsistentNaming
        public IActionResult PagedList(DateTime StartSearchDate, DateTime EndSearchDate, ServiceOperations Operations, int SelectedPageSize, int productPage = 1)
        {
            var model = new OperationServicesListViewModel
            {
                StartSearchDate = StartSearchDate.Date,
                EndSearchDate = EndSearchDate.Date,
                Operations = Operations,
                SelectedPageSize = SelectedPageSize
            };


            var repo = _repository.ServiceOperationsModels.Include(p => p.LineRoutes)
                .Include(p => p.PassangerModels).Where(p => p.ChangeServiceDate >= model.StartSearchDate && p.ChangeServiceDate <= model.EndSearchDate);
            if (model.Operations != ServiceOperations.All)
            {
                repo = repo.Where(p => p.ServiceOperations == model.Operations);
            }
            model.Services = repo.Skip((productPage - 1) * model.SelectedPageSize).Take(model.SelectedPageSize);
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = model.SelectedPageSize,
                TotalItems = repo.Count()
            };
            return View("OperationsList", model);

        }

        public IActionResult DeleteOperation(int id, string returnUrl = null)
        {
            _repository.Delete(id);
            if (returnUrl == null)
            {
                RedirectToAction("OperationsList", new OperationServicesListViewModel());
            }

            int idx = returnUrl.IndexOf('?');

            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now;
            ServiceOperations operations = ServiceOperations.All;
            int pages = 20;
            int productPage = 1;

            if (idx >= 0)
            {
                string query = idx >= 0 ? returnUrl.Substring(idx) : "";
                var queryDictionary = QueryHelpers.ParseQuery(query);
                startDate = DateTime.Parse(queryDictionary["StartSearchDate"], CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                endDate = DateTime.Parse(queryDictionary["EndSearchDate"], CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                if (int.TryParse(queryDictionary["Operations"], out int variable))
                {
                    operations = (ServiceOperations)variable;
                }
                else
                {
                    operations = (ServiceOperations)Enum.Parse(typeof(ServiceOperations), queryDictionary["Operations"]);
                }
                pages = int.Parse(queryDictionary["SelectedPageSize"]);
                if (!queryDictionary.ContainsKey("productPage") || !int.TryParse(queryDictionary["productPage"], out productPage))
                {
                    productPage = 1;
                }
            }


            var model = new OperationServicesListViewModel
            {
                StartSearchDate = startDate.Date,
                EndSearchDate = endDate.Date,
                Operations = operations,
                SelectedPageSize = pages
            };


            var repo = _repository.ServiceOperationsModels.Include(p => p.LineRoutes)
                .Include(p => p.PassangerModels).Where(p => p.ChangeServiceDate >= model.StartSearchDate && p.ChangeServiceDate <= model.EndSearchDate);
            if (model.Operations != ServiceOperations.All)
            {
                repo = repo.Where(p => p.ServiceOperations == model.Operations);
            }
            model.Services = repo.Skip((productPage - 1) * model.SelectedPageSize).Take(model.SelectedPageSize);
            model.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = model.SelectedPageSize,
                TotalItems = repo.Count()
            };

            return RedirectToAction("OperationsList", model);
        }

        public async Task<IActionResult> SaveReportToExcel(string returnUrl)
        {
            if (returnUrl == null)
            {
                RedirectToAction("OperationsList", new OperationServicesListViewModel());
            }

            Debug.Assert(returnUrl != null, nameof(returnUrl) + " != null");
            int idx = returnUrl.IndexOf('?');

            DateTime startDate = DateTime.Now.AddDays(-1);
            DateTime endDate = DateTime.Now;
            ServiceOperations operations = ServiceOperations.All;

            if (idx >= 0)
            {
                string query = idx >= 0 ? returnUrl.Substring(idx) : "";
                Dictionary<string, StringValues> queryDictionary = QueryHelpers.ParseQuery(query);
                startDate = DateTime.Parse(queryDictionary["StartSearchDate"], CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                endDate = DateTime.Parse(queryDictionary["EndSearchDate"], CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal);
                operations = (ServiceOperations)int.Parse(queryDictionary["Operations"]);
            }

            var repo = _repository.ServiceOperationsModels.Include(p => p.LineRoutes)
                .Include(p => p.PassangerModels).Where(p => p.ChangeServiceDate >= startDate && p.ChangeServiceDate <= endDate);
            if (operations != ServiceOperations.All)
            {
                repo = repo.Where(p => p.ServiceOperations == operations);
            }

            ExcelReport report = new ExcelReport();
            string fileName = $"CallCenter_ServiceOperationsTable_{DateTime.Now:ddMMyyy}.xlsx";
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ServiceOperationsModel));
            List<string> types = new List<string>();
            DataTable table = new DataTable();

            table.Columns.Add("Id", typeof(Int32));
            types.Add(typeof(Int32).Name);
            table.Columns.Add("Buchungsdatum", typeof(DateTime));
            types.Add(typeof(DateTime).Name);
            table.Columns.Add("Referenznummer", typeof(Int32));
            types.Add(typeof(Int32).Name);
            table.Columns.Add("Buchungscode", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Ändrerungdatum", typeof(DateTime));
            types.Add(typeof(DateTime).Name);
            table.Columns.Add("Servicearbeiten", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Kunde", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("E-mail", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Zahlungsart", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Art löschen", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Agenturcode", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Verkaufspunkt", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Agentenname", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Zahlungslink", typeof(String));
            types.Add(typeof(String).Name);

            table.Columns.Add("Flugroute", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Fluglinie", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Flugdatum", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Gepäcke", typeof(String));
            types.Add(typeof(String).Name);

            table.Columns.Add("Passagier(e)", typeof(String));
            types.Add(typeof(String).Name);
            table.Columns.Add("Passagieregebühren", typeof(Decimal));
            types.Add(typeof(Decimal).Name);

            table.Columns.Add("Preisdifferenz", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Umbuchungsgebühren", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Zwischensumme Airline", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Bearbeitungsgebühren BERlogic", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Flugpreis inkl. Gebühren", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Nicht erstattbare Taxen", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Zusatzleistung/Gepäck", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Stornogebühren der Airline", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Bereits bezahlte Gebühren-BERlogic", typeof(Decimal));
            types.Add(typeof(Decimal).Name);
            table.Columns.Add("Gutschrift", typeof(Decimal));
            types.Add(typeof(Decimal).Name);

            table.Columns.Add("Rechnungsbetrag EURO", typeof(Decimal));
            types.Add(typeof(Decimal).Name);

            foreach (var item in repo.ToList())
            {
                DataRow row = table.NewRow();

                row["Id"] = item.Id;
                row["Buchungsdatum"] = item.BookingDate;
                row["Referenznummer"] = item.BookingNumber;
                row["Buchungscode"] = item.BookingCode;
                row["Ändrerungdatum"] = item.ChangeServiceDate;
                row["Servicearbeiten"] = item.ServiceOperations.ToString();
                row["Kunde"] = item.CustomerFullName;
                row["E-mail"] = item.CustomerEmail;
                row["Art löschen"] = item.ClearingType;
                row["Agenturcode"] = item.AgencyCode;
                row["Verkaufspunkt"] = item.Salespoint;
                row["Agentenname"] = item.AgentName;
                row["Zahlungsart"] = item.PaymentMethod;

                string routes = string.Empty;
                string lines = string.Empty;
                string dates = string.Empty;
                string luggages = string.Empty;
                foreach (var lineRoute in item.LineRoutes)
                {
                    routes += $"{lineRoute.DepartureAirport}-{lineRoute.ArrivalAirport};";
                    lines += $"{lineRoute.FlightAirline}";
                    if (lineRoute.FlightNumber > 0)
                    {
                        lines += $"{lineRoute.FlightNumber};";
                    }
                    else
                    {
                        lines += ";";
                    }
                    dates +=
                        $"{lineRoute.DepartureDate.ToShortDateString()} {lineRoute.DepartureTime.ToShortTimeString()}-{lineRoute.ArrivalDate.ToShortDateString()} {lineRoute.ArrivalTime.ToShortTimeString()};";
                    if (!string.IsNullOrWhiteSpace(lineRoute.Baggage))
                    {
                        luggages += $"{lineRoute.Baggage};";
                    }
                }
                row["Flugroute"] = routes;
                row["Fluglinie"] = lines;
                row["Flugdatum"] = dates;
                row["Gepäcke"] = luggages;

                string pnrs = String.Empty;
                decimal fees = 0;
                foreach (var passangerModel in item.PassangerModels)
                {
                    pnrs += passangerModel.FullName;
                    fees += passangerModel.PassangerFee;
                }

                row["Passagier(e)"] = pnrs;
                row["Passagieregebühren"] = fees;

                row["Preisdifferenz"] = item.PriceDifference;
                row["Umbuchungsgebühren"] = item.RebookingFee;
                row["Zwischensumme Airline"] = item.AirlineFee;
                row["Bearbeitungsgebühren BERlogic"] = item.BerlogicFee;
                row["Flugpreis inkl. Gebühren"] = item.AirlineTotalPrice;
                row["Nicht erstattbare Taxen"] = item.NonRefundableTaxes;
                row["Zusatzleistung/Gepäck"] = item.AdditionalServicesAndBaggage;
                row["Stornogebühren der Airline"] = item.CancellationAirlineFee;
                row["Bereits bezahlte Gebühren-BERlogic"] = item.PrepaidBerlogicFee;
                row["Gutschrift"] = item.Credit;
                row["Rechnungsbetrag EURO"] = item.TotalFee;
                table.Rows.Add(row);
            }
            var stream = await report.Export(table, fileName, types);

            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}