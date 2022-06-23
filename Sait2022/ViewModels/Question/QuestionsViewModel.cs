using Sait2022.Domain.Model;
using Sait2022.ViewModels.Page;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Question
{
    public class QuestionsViewModel
    {
        public IEnumerable<Questions> Questions { get; set; }

        public PageViewModel PageViewModel { get; set; }

    }
}
