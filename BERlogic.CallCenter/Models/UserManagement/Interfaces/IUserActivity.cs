using System;
using System.Linq;

namespace BERlogic.CallCenter.Models.UserManagement.Interfaces
{
    public interface IUserActivity
    {
        IQueryable<UsersActivity> UsersActivities { get; }
        void SaveActivity(string userId, string activity);
        void SetLoginTime(string userId, DateTime login);
        void SetLogoutTime(string userId, DateTime logout);
        bool CheckStatus();
    }
}
