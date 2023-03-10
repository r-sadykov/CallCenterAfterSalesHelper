#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "LoginViewModel.RememberMe.DisplayName")]
        public bool RememberMe { get; set; }
    }
}
