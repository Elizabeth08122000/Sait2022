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

        /// <summary>
        /// Внешний ключ
        /// </summary>
        public long RangsId { get; set; }
        public Rangs Rangs { get; set; } // навигационное свойство

        /// <summary>
        /// Внутренний ключ вопроса среди одного ранга
        /// </summary>
        public int NumberQuest { get; set; }

        /// <summary>
        /// Сам вопрос
        /// </summary>
        public string ValueQuest { get; set; }

        public Answers Answers { get; set; } //навигационное свойство

        /// <summary>
        /// Ответ ученика
        /// </summary>
        [Required(ErrorMessage = "Укажите ответ на вопрос")]
        public string StudentAnswer { get; set; }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        public bool? CheckAnswer { get; set; }

        /// <summary>
        /// Проверка на пройденность вопроса
        /// </summary>
        public bool IsUsed { get; set; } = false;

        public virtual ICollection<Users> Users { get; set; }
        public Questions()
        {
            Users = new List<Users>();
            StudentAnswers = new List<StudentAnswer>();
        }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
