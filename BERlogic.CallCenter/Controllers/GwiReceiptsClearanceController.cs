using System;
using System.Linq;
using BERlogic.CallCenter.Components;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Repositories;
using BERlogic.CallCenter.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Controllers
{
    [Authorize]
    public class GwiReceiptsClearanceController : Controller
    {
        private readonly IGwiReceiptClearance _repository;

        public GwiReceiptsClearanceController(IGwiReceiptClearance repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new GwiReceiptViewModel());
        }

        [HttpPost]
        public IActionResult Index(GwiReceiptViewModel viewModel)
        {
            IOrderedQueryable<GwiReceipt> repo;
            switch (viewModel.SelectedOption)
            {
                case 0:
                    repo = _repository.GwiReceipts
                        .Where(w => w.BookingDate >= viewModel.StartDate && w.BookingDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                case 1:
                    repo = _repository.GwiReceipts
                        .Where(w => w.ReceivedNoticeDate >= viewModel.StartDate && w.ReceivedNoticeDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                case 2:
                    repo = _repository.GwiReceipts
                        .Where(w => w.ChangesDate >= viewModel.StartDate && w.ChangesDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                case 3:
                    repo = _repository.GwiReceipts
                        .Where(w => w.SepaDate >= viewModel.StartDate && w.SepaDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                default:
                    repo = _repository.GwiReceipts
                        .Where(w => w.BookingDate >= DateTime.Now.AddDays(-14) && w.BookingDate <= DateTime.Now)
                        .OrderBy(o => o.BookingCode);
                    break;
            }
            return ViewComponent(typeof(GwiReceiptsClearanceListViewComponent), repo);
        }

        public IActionResult RefreshResults()
        {
            return null;
        }

        public IActionResult AcceptFilterData(GwiReceiptViewModel viewModel)
        {
            IOrderedQueryable<GwiReceipt> repo;
            switch (viewModel.SelectedOption)
            {
                case 0:
                    repo = _repository.GwiReceipts
                        .Where(w => w.BookingDate >= viewModel.StartDate && w.BookingDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                case 1:
                    repo = _repository.GwiReceipts
                        .Where(w => w.ReceivedNoticeDate >= viewModel.StartDate && w.ReceivedNoticeDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                case 2:
                    repo = _repository.GwiReceipts
                        .Where(w => w.ChangesDate >= viewModel.StartDate && w.ChangesDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                case 3:
                    repo = _repository.GwiReceipts
                        .Where(w => w.SepaDate >= viewModel.StartDate && w.SepaDate <= viewModel.EndDate)
                        .OrderBy(o => o.BookingCode);
                    break;
                default:
                    repo = _repository.GwiReceipts
                        .Where(w => w.BookingDate >= DateTime.Now.AddDays(-14) && w.BookingDate <= DateTime.Now)
                        .OrderBy(o => o.BookingCode);
                    break;
            }
            return ViewComponent(typeof(GwiReceiptsClearanceListViewComponent), repo);
        }

        public IActionResult SaveResultsInExcel()
        {
            return null;
        }
    }
}