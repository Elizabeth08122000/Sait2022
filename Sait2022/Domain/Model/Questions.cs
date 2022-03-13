using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
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
        /// Проверка на пройденность вопроса
        /// </summary>
        public bool IsUsed { get; set; } = false;

        public virtual ICollection<Users> Users { get; set; }
        public Questions()
        {
            Users = new List<Users>();
        }
    }
}
