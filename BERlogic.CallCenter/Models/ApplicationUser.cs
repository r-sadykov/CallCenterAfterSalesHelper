#region Using

using System;
using BERlogic.CallCenter.Models.Enums;
using Microsoft.AspNetCore.Identity;

#endregion

namespace BERlogic.CallCenter.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string UserDefinedName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime ActivationRequest { get; set; } = DateTime.Today;
    }
}
