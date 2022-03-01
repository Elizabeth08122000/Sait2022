using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class Answers:Entity
    {
        /// <summary>
        /// Внутренний ключ ответа среди одного ранга
        /// </summary>
        public int NumberAnswer { get; set; }

        /// <summary>
        /// Сам ответ
        /// </summary>
        public string ValueAnswer { get; set; }

        /// <summary>
        /// Ранг ответа
        /// </summary>
        public char RangAnswer { get; set; }

        public List<Questions> Questions { get; set; }
        public Answers()
        {
            Questions = new List<Questions>();
        }
    }
}
