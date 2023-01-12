using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models
{
    public class GwiReceipt
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DisplayName("PNR")]
        public string BookingCode { get; set; }
        [DisplayName("Passagier(e)")]
        public string Passanger { get; set; }
        [DisplayName("Buchungsdatum")]
        public DateTime BookingDate { get; set; }
        [DisplayName("Mitteilung erhalten Datum")]
        public DateTime ReceivedNoticeDate { get; set; }
        [DisplayName("Änderungsdatum")]
        public DateTime ChangesDate { get; set; }
        [DisplayName("SEPA Code")]
        public string SepaCode { get; set; }
        [DisplayName("SEPA Datum")]
        public DateTime SepaDate { get; set; }
        [DisplayName("SEPA Betrag")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal SepaAmount { get; set; }
        [DisplayName("Mail Inhalt")]
        public string MailContent { get; set; }
    }
}
