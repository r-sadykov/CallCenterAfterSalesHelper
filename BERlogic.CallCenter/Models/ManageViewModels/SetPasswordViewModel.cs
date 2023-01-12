#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.ManageViewModels
{
    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "SetPasswordViewModel.NewPassword.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "SetPasswordViewModel.NewPassword.DisplayName")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "SetPasswordViewModel.ConfirmPassword.DisplayName")]
        [Compare("NewPassword", ErrorMessage = "SetPasswordViewModel.ConfirmPassword.ErrorMessage")]
        public string ConfirmPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}
