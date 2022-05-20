using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class Rangs:Entity
    {
        /// <summary>
        /// Ранг вопроса
        /// </summary>
        [Required(ErrorMessage = "Не указан Ранг")]
        [StringLength(2, MinimumLength = 1, ErrorMessage = "Ранг должен быть от 1 до 2 символов")]
        public string RangQuest { get; set; }

        public ICollection<Questions> Questions { get; set; }
        public ICollection<StudentAnswer> StudentAnswers { get; set; }
        public Rangs()
        {
            Questions = new List<Questions>();
            StudentAnswers = new List<StudentAnswer>();
        }
    }
}
