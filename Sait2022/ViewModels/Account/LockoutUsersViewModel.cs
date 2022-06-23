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
        [Required(ErrorMessage = "Введите Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        public bool IsLocked { get; set; }

        public DateTimeOffset? TimeLockout { get;set; }
    }
}
