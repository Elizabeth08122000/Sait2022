using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Account
{
    public class ChangePasswordViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        [Required(ErrorMessage = "Пароль должен иметь от 6 символов, минимум одно число и символы в верхнем и нижнем регистрах")]
        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Повторение пароля
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }
    }
}
