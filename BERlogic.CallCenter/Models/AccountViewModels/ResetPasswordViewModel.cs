#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.AccountViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "ResetPasswordViewModel.Password.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ResetPasswordViewModel.ConfirmPassword.DisplayName")]
        [Compare("Password", ErrorMessage = "ResetPasswordViewModel.ConfirmPassword.ErrorMessage")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }
}
