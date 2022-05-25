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
                

                List<Questions> questions = new List<Questions>();
                try
                {
                    var studentAnswersDb = db.StudentAnswers
                            .Where(x => x.StudentId == UserId).ToList();
                    var teacherTopic = db.TeacherTopics
                                   .Where(x => x.StudentId == UserId)
                                   .OrderByDescending(x => x.Id)
                                   .Take(3)
                                   .ToList();

                    if (studentAnswersDb.Count > 0)
                        {
                            var studentAnswers = studentAnswersDb
                            .GroupBy(x => x.QuestionsTopicId)
                            .OrderBy(x => x.Key)
                            .ToList();
                        int k = 0;
                        
                        foreach (var answer in studentAnswers)
                            { 
                                k++;
                               
                            if (k == 1)
                            {
                                foreach (var answ in teacherTopic)
                                {
                                    if (answer.LastOrDefault().Result >= 60)
                                    {
                                        if (answ.QuestionsTopicId != null)
                                        {
                                            if (db.Questions.Any(x => x.RangsId > answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId))
                                            {
                                                questions.Add(db.Questions.Where(x => x.RangsId > answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId).FirstOrDefault());
                                            }
                                            else
                                            {
                                                if (db.Questions.Any(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId))
                                                {
                                                    questions.Add(db.Questions.Where(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId).FirstOrDefault());
                                                }
                                                else
                                                {
                                                    questions.Add(db.Questions.Where(x => x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId).FirstOrDefault());
                                                }
                                            }
                                        }
                                        else
                                        {

                                        }

                                    }

                                    else
                                    {
                                        if (answ.QuestionsTopicId != null)
                                        {
                                            if (db.Questions.Any(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId))
                                            {
                                                questions.Add(db.Questions.Where(x => x.Id > answer.LastOrDefault().QuestionId && x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId).FirstOrDefault());
                                            }
                                            else
                                            {
                                                questions.Add(db.Questions.Where(x => x.RangsId == answer.LastOrDefault().RangId && x.QuestionTopcId == answ.QuestionsTopicId).FirstOrDefault());
                                            }
                                        }
                                        else
                                        {

                                        }

                                    }
                                }
                            }
                            else
                            {

                            } 

                            
                            }
                        }
                        else
                        {
                            foreach (var answ in teacherTopic)
                            {
                                if (answ.QuestionsTopicId != null)
                                {
                                    questions.Add(db.Questions.Where(x => x.RangsId == 1 & x.NumberQuest == 1 & x.QuestionTopcId == answ.QuestionsTopicId).FirstOrDefault());
                                }
                                
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
                catch (Exception ex)
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

                    foreach (var quest in questions)
                    {
                        questAnswers.QuestValues.Add(quest.Id, quest.ValueQuest);
                        questAnswers.AnswerValues.Add(quest.Id, "");
                        questAnswers.FilesNameValue.Add(quest.Id, quest.Name);
                        questAnswers.FilesPathValue.Add(quest.Id, quest.Path);
                    }
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
                        var questTopic = await db.TeacherTopics.Where(x => x.QuestionsTopicId == quest.QuestionTopcId).OrderBy(x => x.Id).LastAsync();
                        var studentAnswer = new StudentAnswer()
                        {
                            QuestionId = quest.Id,
                            QuestionsTopicId = (long)questTopic.QuestionsTopicId,
                            RangId = quest.RangsId,
                            StudentId = UserId,
                            Answer = studentAnswerValue.Value
                        };
                        if (questTopic.IsUsedNow)
                        {
                            studentAnswer.TeacherTopicId = questTopic.Id;
                        }
                        else
                        {
                            studentAnswer.TeacherTopicId = default;
                        }

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
                        var questTopic = await db.TeacherTopics.Where(x => x.QuestionsTopicId == ques.QuestionTopcId).OrderBy(x => x.Id).LastAsync();
                        var studAnswer = new StudentAnswer()
                        {
                            QuestionId = ques.Id,
                            QuestionsTopicId = (long)questTopic.QuestionsTopicId,
                            RangId = ques.RangsId,
                            StudentId = UserId,
                            Answer = studAnswerValue.Value
                        };
                        if (questTopic.IsUsedNow)
                        {
                            studAnswer.TeacherTopicId = questTopic.Id;
                        }
                        else
                        {
                            studAnswer.TeacherTopicId = default;
                        }

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
            var topicQ = (from q in db.Questions
                          orderby q.QuestionTopcId
                          join qt in db.QuestionsTopics
                                on q.QuestionTopcId equals qt.Id
                          group new { q, qt } by new { q.QuestionTopcId, qt.Topic, qt.IsUsedNow }
                          into qqt
                          select new
                          {
                              qqt.Key.QuestionTopcId,
                              qqt.Key.Topic,
                              qqt.Key.IsUsedNow
                          });

            QuestQTopicViewModel quest = new QuestQTopicViewModel();

            foreach (var r in topicQ)
            {
                quest.QuestionsTopic.Add(r.QuestionTopcId, r.Topic);
                quest.IsUsedNowDict.Add(r.QuestionTopcId, r.IsUsedNow);
            }

            return View(quest);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateTest(QuestQTopicViewModel questionsTopics)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //var teacherTopic = new TeacherTopic();
                    UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
                    List<QuestionsTopic> listIsUsedNow = new List<QuestionsTopic>();
                    foreach (var topic in questionsTopics.IsUsedNowDict)
                    {
                        var quest = await db.QuestionsTopics.FindAsync(topic.Key);

                        var teachetTopic = new TeacherTopic()
                        {

                            StudentId = UserId,
                            IsUsedNow = topic.Value
                        };
                        if (teachetTopic.IsUsedNow)
                        {
                            teachetTopic.QuestionsTopicId = topic.Key;
                        }
                        else
                        {
                            teachetTopic.QuestionsTopicId = default;
                        }

                        db.TeacherTopics.Add(teachetTopic);
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
            var count = db.TeacherTopics.OrderByDescending(x => x.Id).Take(3).Where(x => x.IsUsedNow==true).Count();
            var answ = db.StudentAnswers.Include("Rangs").Include("Questions").OrderByDescending(x => x.Id).Take(count).OrderBy(x => x.Id);
            var result = db.StudentAnswers.OrderByDescending(x => x.Id)
                                          .Take(count)
                                          .GroupBy(x => x.Result)
                                          .Select(g => new { Result = g.Key });
            foreach(var r in result)
            {
                ViewBag.Message = r.Result.ToString();
                if(r.Result >= 60)
                {
                    ViewBag.Answer = "Молодец! У вас осталось немного до идеального результата!";
                }
                if(r.Result == 100)
                {
                    ViewBag.Answer = "Идеальный результат! Вы гений! Так держать!";
                }
                if(r.Result < 60)
                {
                    ViewBag.Answer = "К сожалению, Вы провалили тест. Попробуйте еще раз!";
                }
            }
            
            return View(await answ.ToListAsync());
        }
    
    }

}

