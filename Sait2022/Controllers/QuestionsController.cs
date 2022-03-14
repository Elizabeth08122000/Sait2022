using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.Models;
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

        /// <summary>
        /// Добавление ответа в бд и его проверка
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CheckAnswer(QuestionsViewModel model, long idQuestion, string valueAnsw)
        {
            if(!ModelState.IsValid)
                return View();

            Questions questions = null;

            foreach(Questions q in db.Questions)
            {
                if(q.Id == model.Id)
                {
                    questions = q;
                }
            }

            var quest = db.Questions
                              .Include(a => a.Answers)
                              .Include(r => r.Rangs)
                              .Include(qt => qt.QuestionsTopic)
                              .Include(u => u.Users)
                              .Where(a => a.Id == idQuestion)
                              .Select(r => new QuestionsViewModel
                              {
                                  Id = r.Id,
                                  NumberQuest = r.NumberQuest,
                                  ValueQuest = r.ValueQuest,
                                  Rangs = (ICollection<Rangs>)r.Rangs,
                                  Answers = (ICollection<Answers>)r.Answers,
                                  QuestionsTopic = (ICollection<QuestionsTopic>)r.QuestionsTopic,
                                  Users = r.Users,
                                  IsUsed = r.IsUsed
                              }).FirstOrDefault();

            db.Answers.Add(new Answers { StudentAnswer = valueAnsw });
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
