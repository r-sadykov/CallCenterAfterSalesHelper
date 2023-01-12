#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.AccountViewModels
{
    public class LoginWith2FaViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "LoginWith2FaViewModel.TwoFactorCode.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "LoginWith2FaViewModel.TwoFactorCode.DisplayName")]
        public string TwoFactorCode { get; set; }

        [Display(Name = "LoginWith2FaViewModel.RememberMachine.DisplayName")]
        public bool RememberMachine { get; set; }

        public bool RememberMe { get; set; }
    }
}
