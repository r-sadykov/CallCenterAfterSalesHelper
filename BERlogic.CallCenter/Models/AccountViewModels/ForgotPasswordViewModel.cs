#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
