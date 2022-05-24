using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.ViewModels.Question
{
    public class QuestQTopicViewModel
    {
        public QuestQTopicViewModel()
        {
            QuestionsId = new Dictionary<long, string>();
            QuestionsTopic = new Dictionary<long, string>();
            IsUsedNow = new Dictionary<long, bool>();
        }
        public Dictionary<long, string> QuestionsId { get; set; }

        public Dictionary<long, string> QuestionsTopic { get; set; }

        public Dictionary<long, bool> IsUsedNow { get; set; }

    }
}
