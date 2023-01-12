using System.Linq;

namespace BERlogic.CallCenter.Models.UserManagement.Interfaces
{
    public interface IUsersInRole
    {
        IQueryable<UsersInRole> UsersInRole { get; }
        IQueryable<UsersInRole> GetList();
    }
}
