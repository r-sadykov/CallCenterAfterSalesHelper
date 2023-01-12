#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.ManageViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ChangePasswordViewModel.OldPassword.DisplayName")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "ChangePasswordViewModel.NewPassword.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "ChangePasswordViewModel.NewPassword.DisplayName")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "ChangePasswordViewModel.ConfirmPassword.DisplayName")]
        [Compare("NewPassword", ErrorMessage = "ChangePasswordViewModel.ConfirmPassword.ErrorMessage")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
