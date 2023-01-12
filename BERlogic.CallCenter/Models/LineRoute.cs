using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models
{
    public class LineRoute
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("LineRoute.DepartureDate.DisplayName")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:d}")]
        [Localizable(true)]
        public DateTime DepartureDate { get; set; }
        [DisplayName("LineRoute.FlightAirline.DisplayName")]
        public string FlightAirline { get; set; }
        [DisplayName("LineRoute.FlightNumber.DisplayName")]
        public int FlightNumber { get; set; }
        [DisplayName("LineRoute.DepartureAirport.DisplayName")]
        public string DepartureAirport { get; set; }
        [DisplayName("LineRoute.ArrivalAirport.DisplayName")]
        public string ArrivalAirport { get; set; }
        [DisplayName("LineRoute.DepartureTime.DisplayName")]
        [DisplayFormat(DataFormatString = "{0:HH':'mm}", ApplyFormatInEditMode = true)]
        public DateTime DepartureTime { get; set; }
        [DisplayName("LineRoute.ArrivalDate.DisplayName")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:d}")]
        public DateTime ArrivalDate { get; set; }
        [DisplayName("LineRoute.ArrivalTime.DisplayName")]
        [DisplayFormat(DataFormatString = "{0:HH':'mm}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalTime { get; set; }
        [DisplayName("LineRoute.Baggage.DisplayName")]
        public string Baggage { get; set; }
    }
}
