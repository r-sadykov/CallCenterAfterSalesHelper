using System;
using System.Collections.Generic;
using System.Linq;
using BERlogic.CallCenter.Data;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class GwiReceiptsRepository:IGwiReceiptClearance
    {
        private readonly ApplicationDbProcessContext _context;

        public GwiReceiptsRepository(ApplicationDbProcessContext context)
        {
            _context = context;
        }

        public IQueryable<GwiReceipt> GwiReceipts => _context.GwiReceipts;

        public void SaveReceipt(GwiReceipt receipt)
        {
            if (receipt.Id == 0)
            {
                _context.GwiReceipts.Add(receipt);
            }
            else
            {
                GwiReceipt dbEntry = _context.GwiReceipts.FirstOrDefault(p => p.Id == receipt.Id);
                if (dbEntry != null)
                {
                    dbEntry.BookingCode = receipt.BookingCode;
                    dbEntry.ChangesDate = receipt.ChangesDate;
                    dbEntry.SepaCode = receipt.SepaCode;
                    dbEntry.BookingDate = receipt.BookingDate;
                    dbEntry.Passanger = receipt.Passanger;
                    dbEntry.ReceivedNoticeDate = receipt.ReceivedNoticeDate;
                    dbEntry.SepaDate = receipt.SepaDate;
                    dbEntry.SepaAmount = receipt.SepaAmount;
                    dbEntry.MailContent = receipt.MailContent;
                }
            }

            _context.SaveChanges();
        }

        public void SaveReceipts(IList<GwiReceipt> receipts)
        {
            _context.GwiReceipts.AddRangeAsync(receipts);
            _context.SaveChangesAsync();
        }

        public IQueryable<GwiReceipt> GetGwiReceiptsInPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.GwiReceipts.Where(w => w.ChangesDate >= startDate && w.ChangesDate <= endDate).OrderBy(o=>o.ChangesDate);
        }
    }
}
