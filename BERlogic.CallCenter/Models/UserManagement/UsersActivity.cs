using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BERlogic.CallCenter.Models.UserManagement
{
    public class UsersActivity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string UserId { get; set; }
        public DateTime Login { get; set; }
        public DateTime Logout { get; set; }
        public bool IsActive { get; set; }
        public string Activity { get; set; }
    }
}
