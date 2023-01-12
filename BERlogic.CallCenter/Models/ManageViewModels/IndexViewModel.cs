#region Using

using System.ComponentModel.DataAnnotations;

#endregion

namespace BERlogic.CallCenter.Models.ManageViewModels
{
    public class IndexViewModel
    {
        [Display(Name="IndexViewModel.Username.DisplayName")]
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "IndexViewModel.Email.DisplayName")]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "IndexViewModel.PhoneNumber.DisplayName")]
        public string PhoneNumber { get; set; }

        public string StatusMessage { get; set; }

        [Display(Name = "IndexViewModel.UserRole.DisplayName")]
        public string UserRole { get; set; }
        [Display(Name = "IndexViewModel.FirstName.DisplayName")]
        public string FirstName { get; set; }
        [Display(Name = "IndexViewModel.LastName.DisplayName")]
        public string LastName { get; set; }
    }
}
