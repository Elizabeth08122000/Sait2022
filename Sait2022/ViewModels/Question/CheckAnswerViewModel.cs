using System.ComponentModel.DataAnnotations;

namespace Sait2022.ViewModels.Question
{
    public class CheckAnswerViewModel
    {

        /// <summary>
        /// Текст вопроса
        /// </summary>
        [Required]
        [Display(Name ="Вопрос")]
        public string ValueQuest { get; set; }

        /// <summary>
        /// Ответ студента
        /// </summary>
        [Required]
        [Display(Name ="Ответа")]
        public string StudentAnswer { get; set; }


    }
}
