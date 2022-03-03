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
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string Patronym { get; set; }

        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Адрес проживания пользователя
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Возвращает полное имя пользователя
        /// </summary>
        public string FullName
        {
            get => FirstName + " " + Surname + " " + Patronym;
        }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string EmailAddress { get; set; }

        public bool IsTeacher { get; set; }

        public bool IsAdministrator { get; set; }

        [ForeignKey("TeacherId")]
        public int TeacherId { get; set; } //внешний ключ справочника Пользователей
        public Employee Employees { get; set; } //навигационное свойство

        public List<Employee> Employeess { get; set; }
        public Employee()
        {
            Employeess = new List <Employee>();
        }
    }
}
