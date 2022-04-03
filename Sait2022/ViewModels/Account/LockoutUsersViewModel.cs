using System;
using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Account
{
    public class LockoutUsersViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool IsLocked { get; set; }

        public DateTimeOffset? TimeLockout { get;set; }
    }
}
