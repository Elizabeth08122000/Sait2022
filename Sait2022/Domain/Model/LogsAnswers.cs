using Sait2022.Domain.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Domain.Model
{
    public class LogsAnswers:Entity
    {
        /// <summary>
        /// Внешний ключ таблицы Users
        /// </summary>
        [ForeignKey("UsersId")]
        public int UsersId { get; set; } //внешний ключ вопросов
        public Users Users { get; set; } //навигационное свойство

        /// <summary>
        /// Внешний ключ таблицы MainOut
        /// </summary>
        [ForeignKey("MainOutId")]
        public int MainOutId { get; set; } //внешний ключ вопросов
        public MainOut MainOut { get; set; } //навигационное свойство
    }
}
