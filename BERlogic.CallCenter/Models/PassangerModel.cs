using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models
{
    public class PassangerModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("PassangerModel.Prefix.DisplayName")]
        public string Prefix { get; set; }
        [DisplayName("PassangerModel.FirstName.DisplayName")]
        public string FirstName { get; set; }
        [DisplayName("PassangerModel.LastName.DisplayName")]
        public string LastName { get; set; }
        [DisplayName("PassangerModel.MiddleName.DisplayName")]
        public string MiddleName { get; set; }
        [NotMapped]
        [DisplayName("PassangerModel.FullName.DisplayName")] public string FullName => Prefix + " " + FirstName + " " + MiddleName + " " + LastName;
        [DisplayName("PassangerModel.PassangerFee.DisplayName")]
        public decimal PassangerFee { get; set; }
    }
}
