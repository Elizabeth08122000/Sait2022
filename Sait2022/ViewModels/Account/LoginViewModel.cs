using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Account
{
    /// <summary>
    /// Модель для авторизации пользователя
    /// </summary>
    public class LoginViewModel
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Required(ErrorMessage = "Введите логин")]
        [Display(Name = "Логин")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Пароль должен иметь от 6 символов, минимум одну цифру и символы в верхнем и нижнем регистрах")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить ли учетную запись в браузере
        /// </summary>
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }

    }
}