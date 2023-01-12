using System.Linq;
using BERlogic.CallCenter.Models;

namespace BERlogic.CallCenter.ViewModels
{
    public class OperationsServiceViewModel
    {
        public ServiceOperationsModel ServiceOperations { get; set; }
        public IQueryable<MailServerConfiguration> MailServer { get; set; }
        public int SelectedItem { get; set; }

    }
}
