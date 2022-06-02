using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class QuestionsTopic: Entity
    {
        /// <summary>
        /// Тема вопроса
        /// </summary>
        [Required(ErrorMessage = "Не указана тема вопроса")]
        [RegularExpression(@"^[А-Яа-я]+$", ErrorMessage = "Некорректная тема вопроса. Введите тему кириллицей")]
        public string Topic { get; set; }

        public bool IsUsedNow { get; set; }

        public ICollection<TeacherTopic> TeacherTopics { get; set; }
        public ICollection<Questions> Questions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public QuestionsTopic()
        {
            Questions = new List<Questions>();
            StudentAnswers = new List<StudentAnswer>();
            TeacherTopics = new List<TeacherTopic>();
        }
    }
}
