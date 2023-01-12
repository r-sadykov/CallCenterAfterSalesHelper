using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BERlogic.CallCenter.Models;
using BERlogic.CallCenter.Models.Enums;

namespace BERlogic.CallCenter.ViewModels
{
    public class ServiceOperationsViewModel
    {
        public int ViewModelId { get; set; }
        [DisplayName("ServiceOperationsViewModel.BookingDate.DisplayName")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:d}")]
        [Localizable(true)]
        public DateTime BookingDate { get; set; }
        [DisplayName("ServiceOperationsViewModel.BookingNumber.DisplayName")]
        public int BookingNumber { get; set; }
        [DisplayName("ServiceOperationsViewModel.BookingCode.DisplayName")]
        public string BookingCode { get; set; }
        [DisplayName("ServiceOperationsViewModel.ChangeServiceDate.DisplayName")]
        [DisplayFormat(ApplyFormatInEditMode = true, ConvertEmptyStringToNull = false, DataFormatString = "{0:d}")]
        [Localizable(true)]
        public DateTime ChangeServiceDate { get; set; }
        [DisplayName("ServiceOperationsViewModel.CustomerFullName.DisplayName")]
        public string CustomerFullName { get; set; }
        [DisplayName("ServiceOperationsViewModel.CustomerEmail.DisplayName")]
        public string CustomerEmail { get; set; }
        [DisplayName("ServiceOperationsViewModel.PaymentMethod.DisplayName")]
        public string PaymentMethod { get; set; }
        [DisplayName("ServiceOperationsViewModel.ClearingType.DisplayName")]
        public string ClearingType { get; set; }
        [DisplayName("ServiceOperationsViewModel.AgencyCode.DisplayName")]
        public string AgencyCode { get; set; }
        [DisplayName("ServiceOperationsViewModel.Salespoint.DisplayName")]
        public string Salespoint { get; set; }
        [DisplayName("ServiceOperationsViewModel.AgentName.DisplayName")]
        public string AgentName { get; set; }
        [DisplayName("ServiceOperationsViewModel.PaymentLink.DisplayName")]
        public string PaymentLink { get; set; }
        [DisplayName("ServiceOperationsViewModel.LineRoutes.DisplayName")]
        public virtual IList<LineRoute> LineRoutes { get; set; }
        [DisplayName("ServiceOperationsViewModel.PassangerModels.DisplayName")]
        public virtual IList<PassangerModelViewModel> PassangerModels { get; set; }
        [DisplayName("ServiceOperationsViewModel.PriceDifference.DisplayName")]
        public string PriceDifference { get; set; }
        [DisplayName("ServiceOperationsViewModel.RebookingFee.DisplayName")]
        public string RebookingFee { get; set; }

        [DisplayName("ServiceOperationsViewModel.AirlineFee.DisplayName")]
        public string AirlineFee { get; set; }
        [DisplayName("ServiceOperationsViewModel.BerlogicFee.DisplayName")]
        public string BerlogicFee { get; set; }
        [DisplayName("ServiceOperationsViewModel.TotalFee.DisplayName")]
        public string TotalFee { get; set; }
        [DisplayName("ServiceOperationsViewModel.ServiceOperations.DisplayName")]
        [EnumDataType(typeof(ServiceOperations))]
        public ServiceOperations ServiceOperations { get; set; }
        [DisplayName("ServiceOperationsViewModel.AirlineTotalPrice.DisplayName")]
        public string AirlineTotalPrice { get; set; }
        [DisplayName("ServiceOperationsViewModel.NonRefundableTaxes.DisplayName")]
        public string NonRefundableTaxes { get; set; }
        [DisplayName("ServiceOperationsViewModel.AdditionalServicesAndBaggage.DisplayName")]
        public string AdditionalServicesAndBaggage { get; set; }
        [DisplayName("ServiceOperationsViewModel.CancellationAirlineFee.DisplayName")]
        public string CancellationAirlineFee { get; set; }
        [DisplayName("ServiceOperationsViewModel.PrepaidBerlogicFee.DisplayName")]
        public string PrepaidBerlogicFee { get; set; }
        [DisplayName("Gutschrift")]
        public string Credit { get; set; }
    }
}
