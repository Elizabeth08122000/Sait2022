using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class Users: IdentityUser<int>
    {
        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public Employee Employee { get; set; }

        public List<LogsAnswers> LogsAnswers { get; set; }
        public Users()
        {
            LogsAnswers = new List<LogsAnswers>();
        }
    }
}
