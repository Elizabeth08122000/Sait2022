using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.ViewModels.Question
{
    public class TeacherTopicViewModel
    {
        public bool IsUsedNow { get; set; }

        public string Topic { get; set; }

        public long Id { get; set; }

        public IEnumerable<TeacherTopic> TeacherTopics { get; set; }

        public IEnumerable<QuestionsTopic> QuestionsTopics { get; set; }

        public IEnumerable<Users> Users { get; set; }


    }
}
