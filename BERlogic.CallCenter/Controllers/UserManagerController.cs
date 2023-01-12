using System.Linq;
using System.Threading.Tasks;
using BERlogic.CallCenter.Data;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.UserManagement.Interfaces;
using BERlogic.CallCenter.Models.UserManagement.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BERlogic.CallCenter.Controllers
{
    public class UserManagerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IUsersInRole _rolesRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagerController(ApplicationDbContext context, IUsersInRole rolesRepository, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _rolesRepository = rolesRepository;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Roles()
        {
            UserInRoleViewModel viewModel = new UserInRoleViewModel
            {
                Users = _rolesRepository.UsersInRole,
                Roles = _context.Roles.AsEnumerable()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> SaveRolesForUser(string userId, string roleId, string oldRoleId)
        {
            var user = _userManager.Users.FirstOrDefault(f => f.Id == userId);
            var role = _roleManager.Roles.FirstOrDefault(f => f.Id == roleId);
            var oldRole = _roleManager.Roles.FirstOrDefault(f => f.Id == oldRoleId);
            await _userManager.RemoveFromRoleAsync(user, oldRole?.Name);
            await _userManager.AddToRoleAsync(user, role?.Name);
            return RedirectToAction("Roles");
        }

        public async Task<IActionResult> DeleteRolesForUser(string userId, string roleId)
        {
            var user = _userManager.Users.FirstOrDefault(f => f.Id == userId);
            var role = _roleManager.Roles.FirstOrDefault(f => f.Id == roleId);
            await _userManager.RemoveFromRoleAsync(user, role?.Name);
            return RedirectToAction("Roles");
        }

    }
}