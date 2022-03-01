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
        /// <summary>
        /// Внешний ключ таблицы QuestionsTopic
        /// </summary>
        [ForeignKey("QuestionsTopicId")]
        public int QuestionsTopicId { get; set; } //внешний ключ справочника вопросов
        public QuestionsTopic QuestionsTopic { get; set; } //навигационное свойство

        /// <summary>
        /// Внутренний ключ вопроса среди одного ранга
        /// </summary>
        public int NumberQuest { get; set; }

        /// <summary>
        /// Сам вопрос
        /// </summary>
        public string ValueQuest { get; set; }

        /// <summary>
        /// Внешний ключ таблицы Rangs
        /// </summary>
        [ForeignKey("RangsId")]
        public int RangsId { get; set; } //внешний ключ справочника рангов
        public Rangs Rangs { get; set; } //навигационное свойство


        /// <summary>
        /// Внешний ключ таблицы Answers
        /// </summary>
        [ForeignKey("AnswersId")]
        public int AnswersId { get; set; } //внешний ключ справочника ответов
        public Answers Answers { get; set; } //навигационное свойство

        public List<MainOut> Main_out { get; set; }
        public Questions()
        {
            Main_out = new List<MainOut>();
        }
    }
}
