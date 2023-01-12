using System.ComponentModel;

namespace BERlogic.CallCenter.Models.Enums
{
    public enum ServiceOperations
    {
        [DisplayName("Rebooking")]
        Rebooking=0,
        [DisplayName("Сancellation")]
        Cancellation=1,
        [DisplayName("Baggage")]
        AddBuggage=2,
        [DisplayName("All services")]
        All=3
    }
}
