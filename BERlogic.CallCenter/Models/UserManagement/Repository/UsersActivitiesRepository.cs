using System;
using System.Linq;
using BERlogic.CallCenter.Models.UserManagement.Interfaces;

namespace BERlogic.CallCenter.Models.UserManagement.Repository
{
    public class UsersActivitiesRepository : IUserActivity
    {
        public IQueryable<UsersActivity> UsersActivities { get; }
        public void SaveActivity(string userId, string activity)
        {
            throw new NotImplementedException();
        }

        public void SetLoginTime(string userId, DateTime login)
        {
            throw new NotImplementedException();
        }

        public void SetLogoutTime(string userId, DateTime logout)
        {
            throw new NotImplementedException();
        }

        public bool CheckStatus()
        {
            throw new NotImplementedException();
        }
    }
}
