using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.ViewModels.Question
{
    public class QuestAnswersViewModel
    {
        public KeyValuePair<long, string> QuestValues { get; set; }
        public KeyValuePair<long, string> AnswerValues { get; set; }

        public PageViewModel PageViewModel { get; set; }
    }
}
