using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.ViewModels.Question
{
    public class QuestionsViewModel
    {
        public long Id { get; set; }

        /// <summary>
        /// Вопросы
        /// </summary>
        public int NumberQuest { get; set; }

        public string ValueQuest { get; set; }

        public bool IsUsed { get; set; }
        /// <summary>
        /// Ранги
        /// </summary>
        public char RangQuest { get; set; }

        /// <summary>
        /// Названия тем вопросов
        /// </summary>
        public string Topic { get; set; }

        /// <summary>
        /// Ответы
        /// </summary>
        public string ValueAnswer { get; set; }

        public string StudentAnswer { get; set; }

        public bool? CheckAnswer { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public string FirstName { get; set; }

        public string Surname { get; set; }
    }
}
