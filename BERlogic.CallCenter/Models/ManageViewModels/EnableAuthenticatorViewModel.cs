#region Using

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.ManageViewModels
{
    public class EnableAuthenticatorViewModel
    {
        [Required]
        [StringLength(7, ErrorMessage = "EnableAuthenticatorViewModel.Code.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "EnableAuthenticatorViewModel.Code.DisplayName")]
        public string Code { get; set; }

        [ReadOnly(true)]
        public string SharedKey { get; set; }

        public string AuthenticatorUri { get; set; }
    }
}
