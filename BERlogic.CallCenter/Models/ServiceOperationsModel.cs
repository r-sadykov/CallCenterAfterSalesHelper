using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BERlogic.CallCenter.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BERlogic.CallCenter.Models
{
    public class ServiceOperationsModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("Buchungsdatum")]
        public DateTime BookingDate { get; set; }
        [DisplayName("Referenznummer")]
        public int BookingNumber { get; set; }
        [DisplayName("Buchungscode")]
        public string BookingCode { get; set; }
        [DisplayName("Ändrerungdatum")]
        public DateTime ChangeServiceDate { get; set; }
        [DisplayName("Kunde")]
        public string CustomerFullName { get; set; }
        [DisplayName("E-mail")]
        public string CustomerEmail { get; set; }
        [DisplayName("Zahlungsart")]
        public string PaymentMethod { get; set; }
        [DisplayName("Art löschen")]
        public string ClearingType { get; set; }
        [DisplayName("Agenturcode")]
        public string AgencyCode { get; set; }
        [DisplayName("Verkaufspunkt")]
        public string Salespoint { get; set; }
        [DisplayName("Agentenname")]
        public string AgentName { get; set; }
        [DisplayName("Zahlungslink")]
        public string PaymentLink { get; set; }
        [DisplayName("Flugroute")]
        [BindRequired]
        public virtual IList<LineRoute> LineRoutes { get; set; }
        [DisplayName("Passagier(e)")]
        [BindRequired]
        public virtual IList<PassangerModel> PassangerModels { get; set; }
        [DisplayName("Preisdifferenz")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal PriceDifference { get; set; }
        [DisplayName("Umbuchungsgebühren")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal RebookingFee { get; set; }

        [DisplayName("Zwischensumme Airline")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal AirlineFee => PriceDifference + RebookingFee;
        [DisplayName("Bearbeitungsgebühren BERlogic")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal BerlogicFee { get; set; }

        [DisplayName("Rechnungsbetrag EURO")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal TotalFee => AirlineFee + BerlogicFee;
        [DisplayName("Servicearbeiten")]
        [EnumDataType(typeof(ServiceOperations))]
        public ServiceOperations ServiceOperations { get; set; }
        [DisplayName("Flugpreis inkl. Gebühren")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal AirlineTotalPrice { get; set; }
        [DisplayName("Nicht erstattbare Taxen")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal NonRefundableTaxes { get; set; }
        [DisplayName("Zusatzleistung/Gepäck")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal AdditionalServicesAndBaggage { get; set; }
        [DisplayName("Stornogebühren der Airline")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal CancellationAirlineFee { get; set; }

        [DisplayName("Bereits bezahlte Gebühren-BERlogic")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal PrepaidBerlogicFee { get; set; }
        [DisplayName("Gutschrift")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public decimal Credit => AirlineTotalPrice - NonRefundableTaxes - AdditionalServicesAndBaggage -
                                 CancellationAirlineFee - BerlogicFee - PrepaidBerlogicFee;

    }
}
