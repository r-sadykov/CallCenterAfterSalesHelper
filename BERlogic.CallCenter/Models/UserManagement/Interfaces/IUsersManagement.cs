using System.Linq;

namespace BERlogic.CallCenter.Models.UserManagement.Interfaces
{
    public interface IUsersManagement
    {
        IQueryable<UsersManagement> Users { get; }
        void SaveUser(UsersManagement user);
        void DeleteUser(string id);
        IQueryable<UsersManagement> GetList();
    }
}
