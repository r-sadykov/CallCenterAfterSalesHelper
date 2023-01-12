using System.Linq;
using BERlogic.CallCenter.Models.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Components
{
    public class GwiReceiptsClearanceListViewComponent : ViewComponent
    {
        public IGwiReceiptClearance Repository { get; set; }

        public GwiReceiptsClearanceListViewComponent(IGwiReceiptClearance repository)
        {
            Repository = repository;
        }
        public IViewComponentResult Invoke()
        {
            return View("GwiReceiptList", Repository.GwiReceipts.OrderBy(x => x.BookingCode));
        }
    }
}