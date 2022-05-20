using Sait2022.Domain.Model.Common;

namespace Sait2022.Domain.Model
{
    public class StudentAnswer : Entity
    {
        /// <summary>
        /// Внешний ключ ученика
        /// </summary>
        public long StudentId { get; set; }
        public Employee Student { get; set; }

        /// <summary>
        /// Внешний ключ вопроса
        /// </summary>
        public long QuestionId { get; set; }
        public Questions Questions { get; set; }

        /// <summary>
        /// Внешний ключ ранга
        /// </summary>
        public long RangId { get; set; }
        public Rangs Rangs { get; set; }

        /// <summary>
        /// Внешний ключ темы
        /// </summary>
        public long QuestionsTopicId { get; set; }
        public QuestionsTopic QuestionsTopic { get; set; }

        /// <summary>
        /// Ответ ученика
        /// </summary>
        public string Answer { get; set; }

        /// <summary>
        /// Проверка ответа
        /// </summary>
        public bool IsCheck { get; set; }

        /// <summary>
        /// Результат теста
        /// </summary>
        public int? Result { get; set; }
    }
}
