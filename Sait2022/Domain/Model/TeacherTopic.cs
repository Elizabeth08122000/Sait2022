using Sait2022.Domain.Model.Common;
using System.Collections.Generic;

namespace Sait2022.Domain.Model
{
    public class TeacherTopic:Entity
    {
        public bool? IsUsedNow { get; set; }

        public long QuestionsTopicId { get; set; }
        public QuestionsTopic QuestionsTopic { get; set; }

        public long StudentId { get; set; }
        public Employee Student { get; set; }

        public TeacherTopic()
        {
            StudentAnswers = new List<StudentAnswer>();
        }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
    }
}
