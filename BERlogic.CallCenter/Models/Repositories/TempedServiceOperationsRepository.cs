using System.Collections.Generic;
using System.Linq;
using BERlogic.CallCenter.ViewModels;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class TempedServiceOperationsRepository : ITempedServiceOperations
    {

        public List<ServiceOperationsViewModel> TempedOperationsModels { get; set; }

        public ServiceOperationsViewModel DeleteRoute(int routeId)
        {
            var route = TempedOperationsModels.LastOrDefault()?.LineRoutes.ToList()[routeId];
            if (route != null)
            {
                TempedOperationsModels.LastOrDefault()?.LineRoutes.Remove(route);
            }
            return TempedOperationsModels.LastOrDefault();
        }

        public ServiceOperationsViewModel AddRoute()
        {
            TempedOperationsModels.LastOrDefault()?.LineRoutes.Add(new LineRoute());
            return TempedOperationsModels.LastOrDefault();
        }

        public void CleanRepository() => TempedOperationsModels = new List<ServiceOperationsViewModel>();
        public void Add(ServiceOperationsViewModel model)
        {
            if (TempedOperationsModels == null) CleanRepository();
            TempedOperationsModels.Add(model);
        }

        public ServiceOperationsViewModel DeletePassanger(int id)
        {
            var passanger = TempedOperationsModels.LastOrDefault()?.PassangerModels.ToList()[id];
            if (passanger != null)
            {
                TempedOperationsModels.LastOrDefault()?.PassangerModels.Remove(passanger);
            }
            return TempedOperationsModels.LastOrDefault();
        }
    }
}
