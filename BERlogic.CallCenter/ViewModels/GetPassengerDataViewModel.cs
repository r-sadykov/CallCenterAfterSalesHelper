using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BERlogic.CallCenter.Models;
using Microsoft.Extensions.Localization;

namespace BERlogic.CallCenter.ViewModels
{
    public class GetPassengerDataViewModel
    {
        public IStringLocalizer Localizer { get; set; }
        public GetPassengerDataViewModel() { }

        public GetPassengerDataViewModel(IStringLocalizer localizer)
        {
            Localizer = localizer;
        }

        [DisplayName("GetPassengerDataViewModel.InputField.DisplayName")]
        [Required(ErrorMessage = "GetPassengerDataViewModel.InputField.ErrorMessage")]
        public string InputField { get; set; }

        public Dictionary<string, string> SearchCriterias => GetCriterias();

        private Dictionary<string, string> GetCriterias()
        {
            Dictionary<string, string> criterias=new Dictionary<string, string>();
            if (Localizer!=null)
            {
                criterias.Add("by PNR", Localizer["by PNR"]);
                criterias.Add("by Booking Number", Localizer["by Booking Number"]);
            }
            else
            {
                criterias.Add("by PNR", "by PNR");
                criterias.Add("by Booking Number", "by Booking Number");
            }

            return criterias;
        }

        [Required(ErrorMessage = "GetPassengerDataViewModel.SelectedItem.ErrorMessage")]
        public string SelectedItem { get; set; }

        public IQueryable<FullReport> PassengersData { get; set; }
    }
}
