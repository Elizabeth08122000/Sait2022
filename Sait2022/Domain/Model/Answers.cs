using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class Answers:Entity
    {
        /// <summary>
        /// Внутренний ключ ответа среди одного ранга
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Сам ответ
        /// </summary>
        [Required]
        public string ValueAnswer { get; set; }

        /// <summary>
        /// Ответ ученика
        /// </summary>
        [Required(ErrorMessage ="Укажите ответ на вопрос")]
        public string? StudentAnswer { get; set; }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        public bool? CheckAnswer { get; set; }

        /// <summary>
        /// внешний ключ таблицы Questions
        /// </summary>
        [ForeignKey("QuestionId")]
        public long QuestionId { get; set; }
        public Questions Questions { get; set; }

    }
}
