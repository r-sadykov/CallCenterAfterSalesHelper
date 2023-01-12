using System;
using System.Linq;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class PassengerMainInfoController : Controller
    {
        private readonly IFullReport _repository;
        private readonly IStringLocalizer<PassengerMainInfoController> _localizer;

        public PassengerMainInfoController(IFullReport repository, IStringLocalizer<PassengerMainInfoController> localizer)
        {
            _repository = repository;
            _localizer = localizer;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            GetPassengerDataViewModel objModel=new GetPassengerDataViewModel(_localizer);
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
                        Int32.TryParse(model.InputField, out var number);
                        reports = _repository.FullReports.Where(p => p.BookingNumber == number);
                        break;
                }

                if (reports!=null && reports.ToList().Any())
                {
                    TempData["message"] = $"{_localizer["Found"]} {reports.Count()} {_localizer["objects"]}";
                    model.PassengersData = reports;
                }
                else
                {
                    TempData["testError"] = $"{_localizer["Do not found any entry!"]}";
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            return View("OrderDetails",_repository.FullReports.FirstOrDefault(p=>p.Id==id));
        }
    }
}