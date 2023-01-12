#region Using

using System;
using System.ComponentModel.DataAnnotations;
using BERlogic.CallCenter.Models.Enums;

#endregion

namespace BERlogic.CallCenter.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "RegisterViewModel.Email.DisplayName")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "RegisterViewModel.Password.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "RegisterViewModel.Password.DisplayName")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "RegisterViewModel.ConfirmPassword.DisplayName")]
        [Compare("Password", ErrorMessage = "RegisterViewModel.ConfirmPassword.ErrorMessage")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "RegisterViewModel.Username.ErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Text)]
        [Display(Name = "RegisterViewModel.Username.DisplayName")]
        public string Username { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "RegisterViewModel.FirstName.ErrorMessage", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "RegisterViewModel.FirstName.DisplayName")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "RegisterViewModel.LastName.ErrorMessage", MinimumLength = 2)]
        [DataType(DataType.Text)]
        [Display(Name = "RegisterViewModel.LastName.DisplayName")]
        public string LastName { get; set; }

        [Required]
        [EnumDataType(typeof(Gender))]
        [Display(Name = "RegisterViewModel.Gender.DisplayName")]
        public Gender Gender { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "RegisterViewModel.ActivationDate.DisplayName")]
        public DateTime ActivationDate { get; set; }=DateTime.Today;
    }
}
