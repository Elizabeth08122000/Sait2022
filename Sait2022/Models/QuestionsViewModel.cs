using Sait2022.Domain.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sait2022.Models
{
    public class QuestionsViewModel
    {
        /// <summary>
        /// Id вопроса
        /// </summary>
        public long QuestionId { get; set; }

        /// <summary>
        /// Сам вопрос
        /// </summary>
        public string ValueQuest { get; set; }

        /// <summary>
        /// Ответ ученика
        /// </summary>
        public ICollection<MainOut> MainOuts { get; set; }

        /// <summary>
        /// Ранг вопроса
        /// </summary>
        public ICollection<Rangs> Rangs { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public ICollection<Answers> Answers { get; set; }

        /// <summary>
        /// Темы вопросов
        /// </summary>
        public ICollection<QuestionsTopic> QuestionsTopic { get; set; }
    }
}
