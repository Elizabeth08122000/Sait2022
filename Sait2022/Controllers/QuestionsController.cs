using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
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
        private long UserId { get; set; }
        private List<Questions> quest_list;
        public QuestionsController(SaitDbContext context)
        {
            db = context;
            UserId = db.Users.First().Id;
            var u = HttpContext;
            var u2 = User;
            var u3 = User.Identity;
            int z = 0;
        }

        /// <summary>
        /// Вывод вопросов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (quest_list == null)
            {
                if(quest_list.Count == 0)
                {
                    quest_list = GetQuestions();
                }
            }
            
            return View(quest_list);
        }

        public async Task<IActionResult> FinishTest()
        {
            quest_list.Clear();
            return RedirectToAction("Index");
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
                    StudentAnswer answer = new StudentAnswer();
                    answer.QuestionId = quest.Id;
                    answer.QuestionsTopicId = quest.QuestionTopcId;
                    answer.RangId = quest.RangsId;
                    answer.Answer = quest.StudentAnswer;
                    answer.StudentId = UserId;
                    Answers answ = db.Answers.Where(x => x.QuestionId == quest.Id).FirstOrDefault();
                    if (answ.ValueAnswer == answer.Answer)
                    {
                        answer.IsCheck = true;
                    }
                    else
                    {
                        answer.IsCheck = false;
                    }
                    db.StudentAnswers.Add(answer);
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

        private List<Questions> GetQuestions()
        {
            var studentAnswers = db.StudentAnswers
                .Where(x => x.StudentId == UserId).AsEnumerable()
                .GroupBy(x => (x.QuestionsTopicId, x.RangId))
                .OrderBy(x => (x.Key.QuestionsTopicId, x.Key.RangId))
                .ToList();
            List<Questions> questions = new List<Questions>();
            if(studentAnswers.Count > 0)
            {
                foreach (var answer in studentAnswers)
                {
                    if (answer.Any(x => x.IsCheck == true))
                    {
                        questions.Add(db.Questions.Where(x => x.RangsId == answer.Key.RangId + 1 && x.QuestionTopcId == answer.Key.QuestionsTopicId).FirstOrDefault());
                    }
                    else
                    {
                        questions.Add(db.Questions.Where(x => x.Id == answer.LastOrDefault().QuestionId && x.QuestionTopcId == answer.Key.QuestionsTopicId).FirstOrDefault());
                    }
                }
            }
            else
            {
                db.Questions
                    .Where(x => x.RangsId == 1)
                    .GroupBy(x => x.QuestionTopcId).ToList()
                    .ForEach(y => questions.Add(y.FirstOrDefault()));
            }

            return questions;
        }

        private bool QuestionsExists(long id)
        {
            return db.Questions.Any(e => e.Id == id);
        }
    
    }

}

