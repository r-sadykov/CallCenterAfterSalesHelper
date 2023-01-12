using System.Security.Claims;
using System.Threading.Tasks;
using BERlogic.CallCenter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BERlogic.CallCenter.Configuration
{
    public class UserSettings:UserClaimsPrincipalFactory<ApplicationUser,IdentityRole>
    {
        public string ImagePath { get; set; } = "/img/avatars/sunny.png";
        public string Role { get; set; }

        public UserSettings(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var identity= await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("FirstName",user.FirstName??""));
            identity.AddClaim(new Claim("LastName", user.LastName ?? ""));
            identity.AddClaim(new Claim("UserName", user.UserName ?? ""));
            identity.AddClaim(new Claim("Email", user.Email ?? ""));

            return identity;
        }
    }
}
