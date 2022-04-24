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
        public QuestionsController(SaitDbContext context)
        {
            db = context;
        }
        private Dictionary<long, string> QuestinsValueDb { get; set; }
        private Dictionary<long, string> AnswersValueDb { get; set; }
        QuestAnswersViewModel questAnswers;

        /// <summary>
        /// Вывод вопросов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            questAnswers = new QuestAnswersViewModel();
            if (QuestinsValueDb.Count == 0)
            {
                UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
                GetQuestionAnswer();
            }
            
            return View(questAnswers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(QuestAnswersViewModel questAnswersViewModel)
        {
            if (ModelState.IsValid)
            {
                //try
                //{
                //    UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
                //    foreach (var studentAnswerValue in questAnswersViewModel.AnswerValues)
                //    {
                //        var quest = await db.Questions.FindAsync(studentAnswerValue.Key);
                //        var studentAnswer = new StudentAnswer()
                //        {
                //            QuestionId = quest.Id,
                //            QuestionsTopicId = quest.QuestionTopcId,
                //            RangId = quest.RangsId,
                //            StudentId = UserId,
                //            Answer = studentAnswerValue.Value
                //        };

                //        Answers answer = db.Answers.FirstOrDefault(x => x.QuestionId == quest.Id);
                //        if (answer.ValueAnswer == studentAnswer.Answer)
                //            studentAnswer.IsCheck = true;
                //        else
                //            studentAnswer.IsCheck = false;

                //        db.StudentAnswers.Add(studentAnswer);
                //    }
                //    await db.SaveChangesAsync();
                //}
                //catch(DbUpdateException ex)
                //{
                //    Console.WriteLine(ex.Message);
                //}
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Quest(int page = 1)
        {
            int pageSize = 1;
            int count = QuestinsValueDb.Count;
            questAnswers.PageViewModel = new PageViewModel(count, page, pageSize);

            questAnswers.QuestValues = QuestinsValueDb.Skip(page - 1).FirstOrDefault();
            questAnswers.AnswerValues = AnswersValueDb.Skip(page - 1).FirstOrDefault();

            return PartialView(questAnswers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Quest(QuestAnswersViewModel model)
        {
            if(ModelState.IsValid)
            {
                AnswersValueDb[model.QuestValues.Key] = model.AnswerValues.Value;
            }
            return PartialView("Quest",model);
        }

        private void GetQuestionAnswer()
        {
            var studentAnswersDb = db.StudentAnswers
                    .Where(x => x.StudentId == UserId).ToList();
            List<Questions> questions = new List<Questions>();
            if(studentAnswersDb.Count > 0)
            {
                var studentAnswers = studentAnswersDb
                .GroupBy(x => x.QuestionsTopicId)
                .OrderBy(x => x.Key)
                .ToList();
                if (studentAnswersDb.Where(x => x.IsCheck).Count() > (0.9 * studentAnswersDb.Count))
                {
                    foreach (var answer in studentAnswers)
                    {
                        if (db.Questions.Any(x => x.RangsId > answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key))
                        {
                            questions.Add(db.Questions.Where(x => x.RangsId > answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key).FirstOrDefault());
                        }
                        else
                        {
                            if (db.Questions.Any(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key))
                            {
                                questions.Add(db.Questions.Where(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key).FirstOrDefault());
                            }
                            else
                            {
                                questions.Add(db.Questions.Where(x => x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key).FirstOrDefault());
                            }
                        }
                    }
                }
                else
                {
                    foreach (var answer in studentAnswers)
                    {
                        if (db.Questions.Any(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key))
                        {
                            questions.Add(db.Questions.Where(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key).FirstOrDefault());
                        }
                        else
                        {
                            questions.Add(db.Questions.Where(x => x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key).FirstOrDefault());
                        }
                    }
                }
            }
            else
            {
                db.Questions
                    .Where(x => x.RangsId == 1 && x.NumberQuest == 1).ToList()
                    .ForEach(y => questions.Add(y));
            }

            foreach (var quest in questions)
            {
                QuestinsValueDb.Add(quest.Id, quest.ValueQuest);
                AnswersValueDb.Add(quest.Id, "");
            }
        }

        private bool QuestionsExists(long id)
        {
            return db.Questions.Any(e => e.Id == id);
        }
    
    }

}

