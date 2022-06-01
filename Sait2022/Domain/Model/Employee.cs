using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class Employee: Entity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "Не указано имя")]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректное имя. Введите имя кириллицей")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Required(ErrorMessage = "Не указана фамилия")]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректная фамилия. Введите имя кириллицей")]
        public string Surname { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректное отчество. Введите имя кириллицей")]
        public string? Patronym { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Адрес проживания пользователя
        /// </summary>
        public string? Address { get; set; }

        /// <summary>
        /// Возвращает полное имя пользователя
        /// </summary>
        public string FullName
        {
            get => FirstName + " " + Surname + " " + Patronym;
        }

        public bool IsTeacher { get; set; }

        public bool IsAdministrator { get; set; }

        public long? TeacherId { get; set; } //внешний ключ справочника Пользователей
        public Employee EmployeesNavig { get; set; } //навигационное свойство

        /// <summary>
        /// Ссылка на Zoom
        /// </summary>
        public string? pathZoom { get; set; }

        public ICollection<Employee> Employeess { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }

        public ICollection<TeacherTopic> TeacherTopics { get; set; }
        public Employee()
        {
            Employeess = new List <Employee>();
            StudentAnswers = new List <StudentAnswer>();
            TeacherTopics = new List <TeacherTopic>();
        }
        
    }
}
