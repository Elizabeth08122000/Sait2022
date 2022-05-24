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
                var TopicUsed = db.TeacherTopics.Where(x => x.StudentId == UserId).LastOrDefault();

                List<Questions> questions = new List<Questions>();

                if ((bool)TopicUsed.IsUsedNow)
                {
                    var studentAnswersDb = db.StudentAnswers
                        .Where(x => x.StudentId == UserId & x.TeacherTopic.IsUsedNow == true).ToList();
                    
                    if (studentAnswersDb.Count > 0)
                    {
                        var studentAnswers = studentAnswersDb
                        .Where(x => x.TeacherTopic.IsUsedNow == true)
                        .GroupBy(x => x.QuestionsTopicId)
                        .OrderBy(x => x.Key)
                        .ToList();
                        foreach (var answer in studentAnswers)
                        {
                            if (answer.LastOrDefault().Result >= 60)
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
                        db.Questions
                            .Where(x => x.RangsId == 1 && x.NumberQuest == 1 && x.QuestionTopcId == TopicUsed.QuestionsTopicId).ToList()
                            .ForEach(y => questions.Add(y));
                    }
                }
                else
                {
                    var studentAnswersDb = db.StudentAnswers
                        .Where(x => x.StudentId == UserId).ToList();

                    if (studentAnswersDb.Count > 0)
                    {
                        var studentAnswers = studentAnswersDb
                        .GroupBy(x => x.QuestionsTopicId)
                        .OrderBy(x => x.Key)
                        .ToList();
                        foreach (var answer in studentAnswers)
                        {
                            if (answer.LastOrDefault().Result >= 60)
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
                        db.Questions
                            .Where(x => x.RangsId == 1 && x.NumberQuest == 1).ToList()
                            .ForEach(y => questions.Add(y));
                    }
                }

                

                foreach (var quest in questions)
                {
                    questAnswers.QuestValues.Add(quest.Id, quest.ValueQuest);
                    questAnswers.AnswerValues.Add(quest.Id, "");
                    questAnswers.FilesNameValue.Add(quest.Id, quest.Name);
                    questAnswers.FilesPathValue.Add(quest.Id, quest.Path);
                }
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
                    int kol = 0;
                    int count = 0;
                    int result = 0;

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

                        if (quest.ValueAnswer == studentAnswer.Answer)
                        {
                            studentAnswer.IsCheck = true;
                            kol += 1;
                            count += 1;
                        }

                        else
                        {
                            studentAnswer.IsCheck = false;
                            count += 1;
                        }
                        
                        studentAnswer.Result = Math.Abs((kol * 100) / count);

                        if (studentAnswer.Result >= 60)
                            result = (int)studentAnswer.Result;
                        else
                            result = (int)studentAnswer.Result;
                    }

                    foreach (var studAnswerValue in questAnswersViewModel.AnswerValues)
                    {
                        var ques = await db.Questions.FindAsync(studAnswerValue.Key);
                        var studAnswer = new StudentAnswer()
                        {
                            QuestionId = ques.Id,
                            QuestionsTopicId = ques.QuestionTopcId,
                            RangId = ques.RangsId,
                            StudentId = UserId,
                            Answer = studAnswerValue.Value
                        };

                        if (ques.ValueAnswer == studAnswer.Answer)
                        {
                            studAnswer.IsCheck = true;
                            kol += 1;
                            count += 1;
                        }

                        else
                        {
                            studAnswer.IsCheck = false;
                            count += 1;
                        }
                        studAnswer.Result = result;

                        db.StudentAnswers.Add(studAnswer);
                    }
                    await db.SaveChangesAsync();
                }
                catch(DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("Result", "Questions");
        }

        [HttpGet]
        public async Task<IActionResult> CreateTest()
        {
            return View(await db.QuestionsTopics.ToListAsync());

            //var topicQ = (from q in db.Questions
            //              join qt in db.QuestionsTopics
            //                    on q.QuestionTopcId equals qt.Id
            //              group new { q, qt } by new { q.QuestionTopcId, qt.Topic }
            //              into qqt
            //              select new
            //              {
            //                  qqt.Key.QuestionTopcId,
            //                  qqt.Key.Topic
            //              });

            //QuestQTopicViewModel quest = new QuestQTopicViewModel();

            //foreach (var r in topicQ)
            //{
            //    quest.QuestionsId.Add(r.QuestionTopcId, r.QuestionTopcId.ToString());
            //    quest.QuestionsTopic.Add(r.QuestionTopcId, r.Topic);
            //}

            //return View(quest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTest([Bind("Topic,IsUsedNow,Id")] QuestionsTopic questionsTopics,TeacherTopic teacherTopic)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
                    if ((bool)questionsTopics.IsUsedNow)
                    {
                        teacherTopic.IsUsedNow = true;
                        teacherTopic.Student.Id = UserId;
                        teacherTopic.QuestionsTopicId = teacherTopic.QuestionsTopicId;
                        db.TeacherTopics.Add(teacherTopic);
                    }
                    else
                    {
                        teacherTopic.IsUsedNow = false;
                        teacherTopic.Student.Id = UserId;
                        teacherTopic.QuestionsTopicId = teacherTopic.QuestionsTopicId;
                        db.TeacherTopics.Add(teacherTopic);
                    }

                    await db.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return RedirectToAction("Index", "Questions");
        }

        [HttpGet]
        public async Task<IActionResult> Result()
        {
            var answ = db.StudentAnswers.Include("Rangs").Include("Questions").OrderByDescending(x => x.Id).Take(3).OrderBy(x => x.Id);
            var result = db.StudentAnswers.OrderByDescending(x => x.Id)
                                          .Take(3)
                                          .GroupBy(x => x.Result)
                                          .Select(g => new { Result = g.Key });
            foreach(var r in result)
            {
                ViewBag.Message = r.Result.ToString();
                if(r.Result > 60)
                {
                    ViewBag.Answer = "Молодец! У вас осталось немного до идеального результата!";
                }
                if(r.Result == 100)
                {
                    ViewBag.Answer = "Идеальный результат! Вы гений! Так держать!";
                }
                else
                {
                    ViewBag.Answer = "К сожалению, Вы провалили тест. Попробуйте еще раз!";
                }
            }
            
            return View(await answ.ToListAsync());
        }
    
    }

}

