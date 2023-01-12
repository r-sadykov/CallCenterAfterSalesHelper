using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models
{
    public class FullReport:IEquatable<FullReport>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("FullReport.BookingNumber.DisplayName")]
        public int BookingNumber { get; set; }
        [DisplayName("FullReport.Status.DisplayName")]
        public string Status { get; set; }
        [DisplayName("FullReport.SystemAgency.DisplayName")]
        public string SystemAgency { get; set; }
        [DisplayName("FullReport.DatevAgencyAccount.DisplayName")]
        public string DatevAgencyAccount { get; set; }
        [DisplayName("FullReport.Gds.DisplayName")]
        public string Gds { get; set; }
        [DisplayName("FullReport.PassengerNames.DisplayName")]
        public string PassengerNames { get; set; }
        [DisplayName("FullReport.PassengerCount.DisplayName")]
        public int PassengerCount { get; set; }
        [DisplayName("FullReport.FirstAirline.DisplayName")]
        public string FirstAirline { get; set; }
        [DisplayName("FullReport.Ticket.DisplayName")]
        public string Ticket { get; set; }
        [DisplayName("FullReport.FirstGdsBookingNumber.DisplayName")]
        public string FirstGdsBookingNumber { get; set; }
        [DisplayName("FullReport.FirstGdsBookingAlias.DisplayName")]
        public string FirstGdsBookingAlias { get; set; }
        [DisplayName("FullReport.BookingDate.DisplayName")]
        public DateTime BookingDate { get; set; }
        [DisplayName("FullReport.FirstRoute.DisplayName")]
        public string FirstRoute { get; set; }
        [DisplayName("FullReport.DepartureDate.DisplayName")]
        public DateTime DepartureDate { get; set; }
        [DisplayName("FullReport.ReturnDate.DisplayName")]
        public DateTime ReturnDate { get; set; }
        [DisplayName("FullReport.Tariff.DisplayName")]
        public decimal Tariff { get; set; }
        [DisplayName("FullReport.Tax.DisplayName")]
        public decimal Tax { get; set; }
        [DisplayName("FullReport.FullScFlex.DisplayName")]
        public decimal FullScFlex { get; set; }
        [DisplayName("FullReport.BloPartScFlex.DisplayName")]
        public decimal BloPartScFlex { get; set; }
        [DisplayName("FullReport.PartnerPartScFlex.DisplayName")]
        public decimal PartnerPartScFlex { get; set; }
        [DisplayName("FullReport.BloFixSc.DisplayName")]
        public decimal BloFixSc { get; set; }
        [DisplayName("FullReport.PartnerFixSc.DisplayName")]
        public decimal PartnerFixSc { get; set; }
        [DisplayName("FullReport.TotalPrice.DisplayName")]
        public decimal TotalPrice { get; set; }
        [DisplayName("FullReport.SellingCurrency.DisplayName")]
        public string SellingCurrency { get; set; }
        [DisplayName("FullReport.ExchangeRateToEuro.DisplayName")]
        public decimal ExchangeRateToEuro { get; set; }
        [DisplayName("FullReport.PaymentMethod.DisplayName")]
        public string PaymentMethod { get; set; }
        [DisplayName("FullReport.SalesPoint.DisplayName")]
        public string SalesPoint { get; set; }
        [DisplayName("FullReport.Agent.DisplayName")]
        public string Agent { get; set; }
        [DisplayName("FullReport.CardType.DisplayName")]
        public string CardType { get; set; }
        [DisplayName("FullReport.CardHolder.DisplayName")]
        public string CardHolder { get; set; }
        [DisplayName("FullReport.CustomerFirstName.DisplayName")]
        public string CustomerFirstName { get; set; }
        [DisplayName("FullReport.CustomerLastName.DisplayName")]
        public string CustomerLastName { get; set; }
        [DisplayName("FullReport.CustomerGender.DisplayName")]
        public string CustomerGender { get; set; }
        [DisplayName("FullReport.CustomerCountry.DisplayName")]
        public string CustomerCountry { get; set; }
        [DisplayName("FullReport.CustomerCity.DisplayName")]
        public string CustomerCity { get; set; }
        [DisplayName("FullReport.CustomerAddress.DisplayName")]
        public string CustomerAddress { get; set; }
        [DisplayName("FullReport.CustomerEmail.DisplayName")]
        public string CustomerEmail { get; set; }
        [DisplayName("FullReport.CustomerPhone.DisplayName")]
        public string CustomerPhone { get; set; }
        [NotMapped]
        [DisplayName("FullReport.CustomerFullName.DisplayName")]
        public string CustomerFullName => CustomerFirstName + " " + CustomerLastName;
        [DisplayName("FullReport.NumberOfSegments.DisplayName")]
        public int NumberOfSegments { get; set; }
        [DisplayName("FullReport.ClearingType.DisplayName")]
        public string ClearingType { get; set; }
        [DisplayName("FullReport.BookingClass.DisplayName")]
        public string BookingClass { get; set; }
        [DisplayName("FullReport.FareBasis.DisplayName")]
        public string FareBasis { get; set; }
        [DisplayName("FullReport.Commission.DisplayName")]
        public string Commission { get; set; }

        #region Implementation of IEquatable<FullReport>

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.</returns>
        public bool Equals(FullReport other)
        {
            if (BookingNumber==other.BookingNumber)
            {
                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hashBookingNumber = BookingNumber == 0 ? 0 : BookingNumber.GetHashCode();

            return hashBookingNumber;
        }

        #endregion
    }
}
