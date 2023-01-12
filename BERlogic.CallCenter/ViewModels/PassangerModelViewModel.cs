using System.ComponentModel;

namespace BERlogic.CallCenter.ViewModels
{
    public class PassangerModelViewModel
    {
        public int Id { get; set; }
        [DisplayName("PassangerModel.Prefix.DisplayName")]
        public string Prefix { get; set; }
        [DisplayName("PassangerModel.FirstName.DisplayName")]
        public string FirstName { get; set; }
        [DisplayName("PassangerModel.LastName.DisplayName")]
        public string LastName { get; set; }
        [DisplayName("PassangerModel.MiddleName.DisplayName")]
        public string MiddleName { get; set; }
        [DisplayName("PassangerModel.FullName.DisplayName")] public string FullName => Prefix + " " + FirstName + " " + MiddleName + " " + LastName;
        [DisplayName("PassangerModel.PassangerFee.DisplayName")]
        public string PassangerFee { get; set; }
    }
}
