using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.ViewModels.Question
{
    public class QuestAnswersViewModel
    {
        public QuestAnswersViewModel()
        {
            QuestValues = new Dictionary<long, string>();
            AnswerValues = new Dictionary<long, string>();
            FilesNameValue = new Dictionary<long, string>();
            FilesPathValue = new Dictionary<long, string>();
        }
        public Dictionary<long, string> QuestValues { get; set; }
        public Dictionary<long, string> AnswerValues { get; set; }

        public Dictionary<long, string> FilesNameValue { get; set; }

        public Dictionary<long, string> FilesPathValue { get; set; }
    }
}
