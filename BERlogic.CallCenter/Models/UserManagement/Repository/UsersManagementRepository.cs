using System;
using System.Linq;
using BERlogic.CallCenter.Models.UserManagement.Interfaces;

namespace BERlogic.CallCenter.Models.UserManagement.Repository
{
    public class UsersManagementRepository : IUsersManagement
    {
        public IQueryable<UsersManagement> Users { get; }
        public void SaveUser(UsersManagement user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UsersManagement> GetList()
        {
            throw new NotImplementedException();
        }
    }
}
