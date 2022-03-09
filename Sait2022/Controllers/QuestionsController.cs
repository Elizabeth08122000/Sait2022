using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
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
            var question = from q in db.Questions
                           join r in db.Rangs on q.Id equals r.Id
                          // join qt in db.QuestionsTopics on q.Id equals qt.Id
                         //  join a in db.Answers on q.Id equals a.Id
                           select new
                           {
                               q.NumberQuest,
                               q.ValueQuest,
                               r.RangQuest
                           };
            Console.WriteLine(question);

            return View(question.ToList());
        }
    }
}
