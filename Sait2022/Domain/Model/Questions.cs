using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class Questions:Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        /// <summary>
        /// Внешний ключ
        /// </summary>
        public long QuestionTopcId { get; set; }
        public QuestionsTopic QuestionsTopic { get; set; } // навигационное свойство

        public string? Name { get; set; }
        public string? Path { get; set; }

        public string? NamePict { get; set; }
        public string? PathPict { get; set; }

        /// <summary>
        /// Внешний ключ
        /// </summary>
        public long RangsId { get; set; }
        public Rangs Rangs { get; set; } // навигационное свойство

        /// <summary>
        /// Внутренний ключ вопроса среди одного ранга
        /// </summary>
        [HiddenInput(DisplayValue = false)]
        public int NumberQuest { get; set; }

        /// <summary>
        /// Сам вопрос
        /// </summary>
        [Required(ErrorMessage = "Не заполнен вопрос")]
        public string ValueQuest { get; set; }

        /// <summary>
        /// Сам ответ
        /// </summary>
        [Required(ErrorMessage ="Не заполнен ответ")]
        public string ValueAnswer { get; set; }

        public virtual ICollection<Users> Users { get; set; }

        public Questions()
        {
            Users = new List<Users>();
            StudentAnswers = new List<StudentAnswer>();
        }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
