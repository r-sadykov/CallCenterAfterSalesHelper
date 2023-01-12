using System.Linq;

namespace BERlogic.CallCenter.Models.Repositories
{
    public interface IServiceOperations
    {
        IQueryable<ServiceOperationsModel> ServiceOperationsModels { get; }
        int Save (ServiceOperationsModel model);
        ServiceOperationsModel Delete (int id);
    }
}
