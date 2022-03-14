using Sait2022.Domain.Model;
using System.Collections.Generic;

namespace Sait2022.Models
{
    public class QuestionsViewModel
    {
        public long Id { get; set; }

        public int NumberQuest { get; set; }

        public string ValueQuest { get; set; }

        public bool IsUsed { get; set; }

        public ICollection<Rangs> Rangs { get; set; }

        public ICollection<QuestionsTopic> QuestionsTopic { get; set; }

        public ICollection<Answers> Answers { get; set; }

        public ICollection<Users> Users { get; set; }

        public ICollection<Employee> Employee { get; set; }
    }
}
