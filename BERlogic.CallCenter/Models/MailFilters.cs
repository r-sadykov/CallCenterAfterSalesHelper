using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models
{
    public class MailFilter
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MailFiltersId { get; set; }
        [DisplayName("MailFilter.MailAddress.DisplayName")]
        [DataType(DataType.EmailAddress, ErrorMessage = "MailServerConfiguration.Address.ErrorMessage")]
        [Required(AllowEmptyStrings = false,ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        public string MailAddress { get; set; }
        [DisplayName("MailFilter.MailThemes.DisplayName")]
        [DataType(DataType.Text)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "MailServerConfiguration.EmptyField.ErrorMessage")]
        public string MailThemes { get; set; }
    }
}
