using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Employee
{
    public class EmployeeViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Почта
        /// </summary>
        [Required]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        [Required]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [Required]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [Required]
        [Display(Name = "Фамилия")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество
        /// </summary>
        [Display(Name = "Отчество")]
        public string Patronym { get; set; }

        /// <summary>
        /// Телефон
        /// </summary>
        [Display(Name = "Телефон")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адрес
        /// </summary>
        [Display(Name = "Адрес")]
        public string Address { get; set; }

        /// <summary>
        /// Является учителем
        /// </summary>
        [Display(Name = "Является учителем?")]
        public bool IsTeacher { get; set; }

        /// <summary>
        /// Является администратором
        /// </summary>
        [Display(Name = "Является администратором?")]
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// Учитель
        /// </summary>
        [Display(Name = "Учитель")]
        public long? TeacherId { get; set; }
    }
}
