using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace BERlogic.CallCenter.Models.UserManagement.ViewModel
{
    public class UserInRoleViewModel
    {
        public IQueryable<UsersInRole> Users { get; set; }
        public IEnumerable<IdentityRole> Roles { get; set; }
        public string SelectedRole { get; set; }
        public string SelectedRoleText { get; set; }
    }
}
