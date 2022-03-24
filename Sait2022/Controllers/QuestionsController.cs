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
        public async Task<IActionResult> QuestionsGet(string returnUrl = null)
        {

            ViewData["ReturnUrl"] = returnUrl;
            return View();
            //использовать join 
            /* List<QuestionsViewModel> QuestionsVMList = new List<QuestionsViewModel>();
             var question = (from Quest in db.Questions
                             join Answ in db.Answers on Quest.Id equals Answ.QuestionId
                             select new
                             {
                                 Quest.QuestionTopcId,
                                 Quest.NumberQuest,
                                 Quest.ValueQuest,
                                 Answ.NumberAnswer,
                                 Answ.ValueAnswer,
                                 Answ.CheckAnswer
                             }).AsEnumerable().GroupBy(x => x.QuestionTopcId);
             foreach (var item in question)
             {
                 QuestionsViewModel qvm = new QuestionsViewModel();
                 qvm.ValueQuest = item.First().ValueQuest;
                 qvm.NumberQuest = item.First().NumberQuest;
                 qvm.ValueAnswer = item.First().ValueAnswer;
                 qvm.CheckAnswer = item.First().CheckAnswer;
                 QuestionsVMList.Add(qvm);
             }
             return View("Questions", QuestionsVMList);

             /*var question = db.Questions.Select(x => new
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
             }*/


        }

        /// <summary>
        /// Добавление ответа в бд и его проверка
        /// </summary>
        /// <returns></returns>


        /*public ActionResult CheckAnswer(long id, [Bind("StudentAnswer")] Answers answers)
        {
            if (id != answers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(answers);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswersExists(answers.Id))
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
            ViewData["QuestionId"] = new SelectList(db.Questions, "Id", "ValueQuest", answers.QuestionId);
            return View(answers);
        }


        [HttpGet("CheckAnswer/{Id}")]
        public IActionResult CheckAnswer(long Id)
        {
            ViewBag.Id = Id;
            return View();
        }*/

        [HttpPost]       
        public IActionResult CheckAnswer(QuestionsViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

                    var answ = db.Answers
                                 .Where(x => x.QuestionId == model.Id)
                                 .Select(x =>
                    new Answers
                    {
                        StudentAnswer = model.StudentAnswer
                    }).AsEnumerable();
                    db.UpdateRange(answ);
                    db.SaveChanges();
   
            return RedirectToAction("Index");
            /*  return View("Questions");

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

              return RedirectToAction("Index");*/
            /*  private bool AnswersExists(long id)
              {
                  return db.Answers.Any(e => e.Id == id);
              }*/
        }

    }
}
