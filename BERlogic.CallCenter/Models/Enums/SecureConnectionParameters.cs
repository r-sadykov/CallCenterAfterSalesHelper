using System.ComponentModel.DataAnnotations;

namespace BERlogic.CallCenter.Models.Enums
{
    public enum SecureConnectionParameters
    {
        [Display(Name = "NONE")]
        None=0,
        [Display(Name = "Auto")]
        Auto=1,
        [Display(Name = "SSL / TLS")]
        Ssltls=4,
        [Display(Name = "SSL")]
        Ssl=2,
        [Display(Name = "STARTTLS")]
        Starttls=3
    }
}
