using System.Collections.Generic;
using System.Linq;
using BERlogic.CallCenter.Data;
using BERlogic.CallCenter.Models.UserManagement.Interfaces;

namespace BERlogic.CallCenter.Models.UserManagement.Repository
{
    public class UsersInRoleRepository : IUsersInRole
    {
        private readonly ApplicationDbContext _context;

        public UsersInRoleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<UsersInRole> UsersInRole => GetList();

        public IQueryable<UsersInRole> GetList()
        {
            var users = _context.Users.ToList();
            List<UsersInRole> roles = new List<UsersInRole>();
            foreach (var user in users)
            {
                if (user != null)
                {
                    var userRoles = _context.UserRoles.Where(c => c.UserId == user.Id).OrderBy(o => o.RoleId).ToList();
                    if (userRoles.Count == 0)
                    {
                        UsersInRole role = new UsersInRole
                        {
                            UserId = user.Id,
                            FullName = $"{user.FirstName} {user.LastName}",
                            RoleId = null,
                            Role = null
                        };
                        roles.Add(role);
                    }
                    else
                    {
                        for (int i = 0; i < userRoles.Count; i++)
                        {
                            UsersInRole role = new UsersInRole
                            {
                                UserId = user.Id,
                                FullName = $"{user.FirstName} {user.LastName}",
                                RoleId = userRoles[i].RoleId,
                                Role = _context.Roles.FirstOrDefault(f => f.Id == userRoles[i].RoleId)?.Name
                            };
                            roles.Add(role);
                        }
                    }

                }
            }

            return roles.AsQueryable();
        }
    }
}
