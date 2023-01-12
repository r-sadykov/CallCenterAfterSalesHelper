#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.AccountViewModels
{
    public class LoginWithRecoveryCodeViewModel
    {
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "LoginWithRecoveryCodeViewModel.RecoveryCode.DisplayName")]
        public string RecoveryCode { get; set; }
    }
}
