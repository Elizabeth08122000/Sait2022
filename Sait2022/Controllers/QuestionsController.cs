using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using System;
using System.Collections.Generic;
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
        public IActionResult QuestionsGet()
        {
            var question = db.Questions.Select(x => new
                            {
                                Id = x.QuestionTopcId,
                                NumberQuest = x.NumberQuest,
                                ValueQuest = x.ValueQuest
                            }).AsEnumerable().GroupBy(x => x.Id);

            List<Questions> quest_list = new List<Questions>();

            foreach (var item in question)
            {
                Questions quest = new Questions();
                quest.QuestionTopcId = item.ToList()[0].Id;
                quest.NumberQuest = item.Select(c => c.NumberQuest).First();
                quest.ValueQuest = item.Select(c => c.ValueQuest).First();
                quest_list.Add(quest);
            }

            return View("Questions", quest_list);
        }

        [HttpPost]
        public IActionResult CheckAnswer()
        {
            return View();
        }
    }
}
