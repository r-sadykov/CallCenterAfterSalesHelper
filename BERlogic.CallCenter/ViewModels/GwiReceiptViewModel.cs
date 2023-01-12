using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BERlogic.CallCenter.ViewModels
{
    public class GwiReceiptViewModel
    {
        [DisplayName("Beginnendatum:")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; } = DateTime.Now.AddDays(-7);
        [DisplayName("Endedatum:")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EndDate { get; set; }= DateTime.Now;
        public List<string> CheckOptionList=>new List<string>{"Buchungsdatum", "Mitteilung erhalten Datum", "Änderungsdatum", "SEPA Datum" };
        public int SelectedOption { get; set; }
    }
}
