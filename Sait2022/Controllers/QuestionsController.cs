using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.ViewModels.Question;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Controllers
{
    public class QuestionsController : Controller
    {
        private readonly SaitDbContext db;

        public QuestionsController(SaitDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var question = db.Questions.Where(r => r.IsUsed == false).Select(x => new
            {
                id = x.Id,
                IdTopic = x.QuestionTopcId,
                NumberQuest = x.NumberQuest,
                ValueQuest = x.ValueQuest,
                IsUsed = x.IsUsed,
                RandId = x.RangsId
            }).AsEnumerable().GroupBy(x => x.IdTopic);
            List<Questions> quest_list = new List<Questions>();
            foreach (var item in question)
            {
                Questions quest = new Questions();
                quest.Id = item.ToList()[0].id;
                quest.QuestionTopcId = item.ToList()[0].IdTopic;
                quest.NumberQuest = item.Select(c => c.NumberQuest).First();
                quest.ValueQuest = item.Select(c => c.ValueQuest).First();
                quest.RangsId = item.Select(c => c.RandId).First();
                quest_list.Add(quest);
            }
            return View(quest_list);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await db.Questions.FindAsync(id);
            if (quest == null)
            {
                return NotFound();
            }
            return View(quest);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("StudentAnswer,IsUsed,Id")] Questions quest)
        {
            if (id != quest.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Answers answ = db.Answers.Where(x => x.QuestionId == quest.Id).FirstOrDefault();
                    Questions dataModel = db.Questions.Where(x => x.Id == quest.Id).FirstOrDefault();
                    dataModel.StudentAnswer = quest.StudentAnswer;
                    dataModel.IsUsed = true;
                    if (answ.ValueAnswer == dataModel.StudentAnswer)
                    {
                        dataModel.CheckAnswer = true;
                    }
                    else
                    {
                        dataModel.CheckAnswer = false;
                    }
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(quest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quest);
        }

        private bool QuestionsExists(long id)
        {
            return db.Questions.Any(e => e.Id == id);
        }
    
    }

}

