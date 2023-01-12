using System;
using System.Collections.Generic;
using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IGwiReceiptClearance
    {
        IQueryable<GwiReceipt> GwiReceipts { get; }
        void SaveReceipt(GwiReceipt receipt);
        void SaveReceipts(IList<GwiReceipt> receipts);
        IQueryable<GwiReceipt> GetGwiReceiptsInPeriod(DateTime startDate, DateTime endDate);
    }
}
