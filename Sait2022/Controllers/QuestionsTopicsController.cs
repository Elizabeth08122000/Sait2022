using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Controllers
{
    [Controller]
    public class QuestionsTopicsController:Controller
    {
        private readonly SaitDbContext _saitDbContext;

        public QuestionsTopicsController(SaitDbContext _saitDbContext)
        {
            _saitDbContext = _saitDbContext ?? throw new ArgumentNullException(nameof(_saitDbContext));
        }

        public IActionResult QuestionsTopics()
        {
            QuestionsTopic qTopics = new QuestionsTopic();
            ViewBag.Message = qTopics.Topic;
            return View();
        }
    }
}
