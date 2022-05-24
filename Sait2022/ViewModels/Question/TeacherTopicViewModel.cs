using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.ViewModels.Question
{
    public class TeacherTopicViewModel
    {
        public IEnumerable<TeacherTopic> TeacherTopics { get; set; }

        public IEnumerable<QuestionsTopic> QuestionsTopics { get; set; }

        public IEnumerable<Users> Users { get; set; }


    }
}
