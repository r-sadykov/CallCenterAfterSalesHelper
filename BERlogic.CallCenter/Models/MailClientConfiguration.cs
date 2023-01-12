using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models
{
    public class MailClientConfiguration
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ConfigurationId { get; set; }
        [Required]
        [MaxLength(20)]
        [DisplayName(displayName: "MailClientConfiguration.ConfigurationName.DisplayName")]
        [DataType(DataType.Text)]
        public string ConfigurationName { get; set; }
        [Required]
        [DisplayName(displayName: "MailClientConfiguration.SourceFolder.DisplayName")]
        [DataType(DataType.Text)]
        public string SourceFolder { get; set; }
        [DisplayName(displayName: "MailClientConfiguration.TargetFolder.DisplayName")]
        [DataType(DataType.Text)]
        public string TargetFolder { get; set; }
    }
}
