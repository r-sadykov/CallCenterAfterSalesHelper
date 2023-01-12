using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;

namespace BERlogic.CallCenter.Models.Enums
{
    public enum Gender
    {
        [Display(Name = "Gender")]
        None = 0,
        [Display(Name = "Male")]
        Male = 1,
        [Display(Name = "Female")]
        Female = 2,
        [Display(Name = "Prefer not to answer")]
        Unknown = 3
    }
}
