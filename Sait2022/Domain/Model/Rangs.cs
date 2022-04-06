using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class Rangs:Entity
    {
        /// <summary>
        /// Ранг вопроса
        /// </summary>
        public char RangQuest { get; set; }

        public ICollection<Questions> Questions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public Rangs()
        {
            Questions = new List<Questions>();
            StudentAnswers = new List<StudentAnswer>();
        }
    }
}
