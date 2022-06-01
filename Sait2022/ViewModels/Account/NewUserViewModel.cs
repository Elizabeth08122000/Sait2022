using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Account
{
    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    public class NewUserViewModel
    {
        /// <summary>
        /// Почта
        /// </summary>
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage ="Пароль должен иметь от 6 символов, минимум одну цифру и символы в верхнем и нижнем регистрах")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        /// <summary>
        /// Повторение пароля
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждение пароля")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [Display(Name = "Логин")]
        public string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required(ErrorMessage = "Не указано имя")]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректное имя. Введите имя кириллицей")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required(ErrorMessage = "Не указана фамилия")]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректная фамилия. Введите фамилию кириллицей")]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректное отчество. Введите отчество кириллицей")]
        [Display(Name = "Отчество")]
        public string Patronym { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Является учителем?")]
        public bool IsTeacher { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Является администратором?")]
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Учитель")]
        public long? TeacherId { get; set; }

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Display(Name = "Zoom")]
        public string? PathZoom { get; set; }
    }
}