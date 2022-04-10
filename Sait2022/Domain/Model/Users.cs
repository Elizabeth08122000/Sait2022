using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class Users: IdentityUser<int>
    {
        public long EmployeeId { get; set; }
        /// <summary>
        /// Профиль ученика
        /// </summary>
        public Employee Employee { get; set; }

        public virtual ICollection<Questions> Questions { get; set; }
        public Users()
        {
            Questions = new List<Questions>();
        }

    }
}
