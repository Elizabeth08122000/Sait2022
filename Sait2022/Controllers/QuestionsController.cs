﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.ViewModels.Pages;
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

        /// <summary>
        /// Вывод вопросов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            

            QuestAnswersViewModel questAnswers = new QuestAnswersViewModel();
            if (questAnswers.QuestValues.Count == 0)
            {
                UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
                GetQuestionsAnswers(questAnswers);
            }
            
            

            return View(questAnswers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(QuestAnswersViewModel questAnswersViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
                    foreach (var studentAnswerValue in questAnswersViewModel.AnswerValues)
                    {
                        var quest = await db.Questions.FindAsync(studentAnswerValue.Key);
                        var studentAnswer = new StudentAnswer()
                        {
                            QuestionId = quest.Id,
                            QuestionsTopicId = quest.QuestionTopcId,
                            RangId = quest.RangsId,
                            StudentId = UserId,
                            Answer = studentAnswerValue.Value
                        };
                        

                        Answers answer = db.Answers.FirstOrDefault(x => x.QuestionId == quest.Id);
                        if (answer.ValueAnswer == studentAnswer.Answer)
                            studentAnswer.IsCheck = true;
                        else
                            studentAnswer.IsCheck = false;

                        db.StudentAnswers.Add(studentAnswer);
                    }
                    await db.SaveChangesAsync();
                }
                catch(DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("Index");
        }

        private void GetQuestionsAnswers(QuestAnswersViewModel model, int page = 1)
        {
            int pageSize = 1;   // количество элементов на странице
            var studentAnswersDb = db.StudentAnswers
                .Where(x => x.StudentId == UserId).ToList();
            List<Questions> questions = new List<Questions>();
            if(studentAnswersDb.Count > 0)
            {
                var studentAnswers = studentAnswersDb
                .GroupBy(x => x.QuestionsTopicId)
                .OrderBy(x => x.Key)
                .ToList();
                foreach (var answer in studentAnswers)
                {
                    if(answer.LastOrDefault().IsCheck == true)
                    {
                        if(db.Questions.Any(x => x.RangsId > answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key))
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
                    else
                    {
                        if(db.Questions.Any(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answer.Key))
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
                    .Where(x => x.RangsId == 2 && x.NumberQuest == 1).ToList()
                    .ForEach(y => questions.Add(y));
            }
            var count = questions.Count();
            var items = questions.ToList();
            PagesViewModel pagesViewModel = new PagesViewModel(count, page, pageSize);
            foreach (var quest in items)
            {
                model.QuestValues.Add(quest.Id, quest.ValueQuest);
                model.AnswerValues.Add(quest.Id, "");
                model.PagesViewModel = pagesViewModel;
            }
        }

        private bool QuestionsExists(long id)
        {
            return db.Questions.Any(e => e.Id == id);
        }
    
    }

}

