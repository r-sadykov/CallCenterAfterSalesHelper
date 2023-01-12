using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BERlogic.CallCenter.Models.Enums;

namespace BERlogic.CallCenter.Models
{
    public class MailServerConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationId { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.ConfigurationName.DisplayName")]
        [DataType(DataType.Text)]
        public string ConfigurationName { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.UserName.DisplayName")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.Address.DisplayName")]
        [DataType(DataType.EmailAddress, ErrorMessage = "MailServerConfiguration.Address.ErrorMessage")]
        public string Address { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.Password.DisplayName")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.ServerNameOfIncomeMessages.DisplayName")]
        [DataType(DataType.Url, ErrorMessage = "MailServerConfiguration.UrlLink.ErrorMessage")]
        public string ServerNameOfIncomeMessages { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.ServerPortOfIncomeMessages.DisplayName")]
        [Range(0,Int32.MaxValue,ErrorMessage = "MailServerConfiguration.IsNumeric.ErrorMessage")]
        public int ServerPortOfIncomeMessages { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.ServerNameOfOutcomeMessages.DisplayName")]
        [DataType(DataType.Url, ErrorMessage = "MailServerConfiguration.UrlLink.ErrorMessage")]
        public string ServerNameOfOutcomeMessages { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.ServerPortOfOutcomeMessages.DisplayName")]
        [Range(0, Int32.MaxValue, ErrorMessage = "MailServerConfiguration.IsNumeric.ErrorMessage")]
        public int ServerPortOfOutcomeMessages { get; set; }
        [Required(ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        [DisplayName(displayName: "MailServerConfiguration.ServerSecureConnectionParameters.DisplayName")]
        [EnumDataType(typeof(SecureConnectionParameters))]
        public SecureConnectionParameters ServerSecureConnectionParameters { get; set; }
        [DisplayName(displayName: "MailServerConfiguration.UseNameAndPassword.DisplayName")]
        public bool UseNameAndPassword { get; set; }
        [DisplayName(displayName: "MailServerConfiguration.UsedNameForEmailConnection.DisplayName")]
        [DataType(DataType.Text)]
        public string UsedNameForEmailConnection { get; set; }
        [DisplayName(displayName: "MailServerConfiguration.UseSecureAuthentication.DisplayName")]
        [Range(typeof(bool), "false", "true")]
        public bool UseSecureAuthentication { get; set; }
    }
}
