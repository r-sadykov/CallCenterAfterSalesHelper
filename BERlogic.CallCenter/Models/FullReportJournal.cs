using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace BERlogic.CallCenter.Models
{
    [DataContract]
    public class FullReportJournal
    {
        [DataMember]
        [Display(Name = "ID:")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.RegistrationEventDate.DisplayName")]
        public DateTime RegistrationEventDate { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.FileName.DisplayName")]
        public string FileName { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.Pnr_Before.DisplayName")]
        public string Pnr_Before { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.Pnr_After.DisplayName")]
        public string Pnr_After { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.BookingNumber_Before.DisplayName")]
        public int BookingNumber_Before { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.BookingNumber_After.DisplayName")]
        public int BookingNumber_After { get; set; }
        [DataMember]
        [Display(Name = "FullReportJournal.FileUploadDate.DisplayName")]
        public DateTime FileUploadDate { get; set; }
    }
}
