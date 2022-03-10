using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using System;
using System.Linq;

namespace Sait2022.Controllers
{
    public class QuestionsController:Controller
    {
        private readonly SaitDbContext db;

        public QuestionsController(SaitDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult Questions()
        {
            var question = db.Questions.Select(x => new
            {
                x.NumberQuest,
                x.ValueQuest
            });
            Questions result = new Questions();
            foreach(var item in question)
            {
                result.NumberQuest = item.NumberQuest;
                result.ValueQuest = item.ValueQuest;
            }

            return View("Questions",(object)result.ToString());
        }
    }
}
