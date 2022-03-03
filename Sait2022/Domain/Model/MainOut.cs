using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class MainOut:Entity
    {
        /// <summary>
        /// Внутренний ключ ответа среди одного ранга
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Внешний ключ таблицы Questions
        /// </summary>
        [ForeignKey("QuestionsId")]
        public int QuestionsId { get; set; } //внешний ключ вопросов
        public Questions Questions { get; set; } //навигационное свойство

        /// <summary>
        /// Ответ ученика
        /// </summary>
        public string ValueAnswer { get; set; }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        public bool CheckAnswer { get; set; }

        public List<LogsAnswers> LogsAnswers { get; set; }
        public MainOut()
        {
            LogsAnswers = new List<LogsAnswers>();
        }
    }
}
