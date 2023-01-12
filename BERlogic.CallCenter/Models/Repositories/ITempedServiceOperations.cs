using System.Collections.Generic;
using BERlogic.CallCenter.ViewModels;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface ITempedServiceOperations
    {
        List<ServiceOperationsViewModel> TempedOperationsModels { get; set; }
        ServiceOperationsViewModel DeleteRoute(int routeId);
        ServiceOperationsViewModel AddRoute();
        void CleanRepository();
        void Add(ServiceOperationsViewModel model);
        ServiceOperationsViewModel DeletePassanger(int id);
    }
}
