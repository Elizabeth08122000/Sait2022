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
        private List<StudentAnswer> studentAnswers_list;
        public QuestionsController(SaitDbContext context)
        {
            db = context;
            studentAnswers_list = new List<StudentAnswer>();
            quest_list = new List<Questions>();
        }

        /// <summary>
        /// Вывод вопросов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (quest_list.Count == 0)
            {
                UserId = long.Parse(User.Identity.GetUserId());
                quest_list = GetQuestions();
            }


            return View(quest_list);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string submit)
        {
            if(studentAnswers_list.Count > 0 && submit == "Save")
            {
                db.StudentAnswers.AddRange(studentAnswers_list);
                db.SaveChanges();
                studentAnswers_list.Clear();
                quest_list.Clear();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quest = await db.Questions.FindAsync(id);
            var studentAnswer = studentAnswers_list.Any(x => x.QuestionId == id) ? studentAnswers_list.LastOrDefault(x=>x.QuestionId == id) : new StudentAnswer() 
            { 
                QuestionId = quest.Id,
                RangId = quest.RangsId,
                QuestionsTopicId = quest.QuestionTopcId,
                StudentId = UserId,
                Answer = ""
            };
            if (quest == null)
            {
                return NotFound();
            }
            var questStudentAnswer = new KeyValuePair<Questions, StudentAnswer>(quest, studentAnswer); 
            return View(questStudentAnswer);
        }

        // POST: Answers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, KeyValuePair<Questions,StudentAnswer> questStudentAnswer)
        {
            if (id != questStudentAnswer.Key.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Answers answ = db.Answers.Where(x => x.QuestionId == questStudentAnswer.Key.Id).FirstOrDefault();
                    if (answ.ValueAnswer == questStudentAnswer.Value.Answer)
                    {
                        questStudentAnswer.Value.IsCheck = true;
                    }
                    else
                    {
                        questStudentAnswer.Value.IsCheck = false;
                    }

                    if (studentAnswers_list.Any(x => x.QuestionId == questStudentAnswer.Key.Id))
                    {
                        studentAnswers_list.FirstOrDefault(x => x.QuestionId == questStudentAnswer.Key.Id).Answer = questStudentAnswer.Value.Answer;
                    }
                    else
                    {
                        studentAnswers_list.Add(questStudentAnswer.Value);
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuestionsExists(questStudentAnswer.Key.Id))
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
            return View(questStudentAnswer);
        }

        private List<Questions> GetQuestions()
        {
            var studentAnswers = db.StudentAnswers
                .Where(x => x.StudentId == UserId).ToList()
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
                        questions.Add(db.Questions.Where(x => x.RangsId > answer.Key.RangId && x.QuestionTopcId == answer.Key.QuestionsTopicId).FirstOrDefault());
                    }
                    else
                    {
                        questions.Add(db.Questions.Where(x => x.Id > answer.LastOrDefault().QuestionId && x.QuestionTopcId == answer.Key.QuestionsTopicId).FirstOrDefault());
                    }
                }
            }
            else
            {
                db.Questions
                    .Where(x => x.RangsId == 1).ToList()
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

