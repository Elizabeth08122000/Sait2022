using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.Repositories;
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
        private IAnswersRepository answersRepository = new StaticAnswersRepository();

        public QuestionsController(SaitDbContext context)
        {
            db = context;
        }

        public ActionResult Index()
        {
            List<QuestionsViewModel> QuestionsVMList = new List<QuestionsViewModel>();
            var question = (from Quest in db.Questions
                            join Answ in db.Answers on Quest.Id equals Answ.QuestionId
                            join QTopic in db.QuestionsTopics on Quest.QuestionTopcId equals QTopic.Id
                            select new
                            {
                                QTopic.Topic,
                                Quest.NumberQuest,
                                Quest.ValueQuest,
                                Answ.NumberAnswer,
                                Answ.ValueAnswer,
                                Answ.CheckAnswer
                            }).ToList();
            foreach (var item in question)
            {
                QuestionsViewModel qvm = new QuestionsViewModel();
                qvm.ValueQuest = item.ValueQuest;
                qvm.NumberQuest = item.NumberQuest;
                qvm.ValueAnswer = item.ValueAnswer;
                qvm.CheckAnswer = item.CheckAnswer;
                QuestionsVMList.Add(qvm);
            }
            return View("Questions", QuestionsVMList.AsEnumerable());
        }

        [HttpGet]
        public ActionResult Edit(long ids)
        {
            var answer = answersRepository.GetById(ids);
            if (answer == null) return new StatusCodeResult(401);
            return View(ViewModelFromAnswers(answer));
        }

        [HttpPost]
        public IActionResult Edit(QuestionsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var IdAnswer = answersRepository.GetById(viewModel.Id);
                UpdateAnswer(IdAnswer, viewModel);
                answersRepository.Upsert(IdAnswer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        private QuestionsViewModel ViewModelFromAnswers(Answers answers)
        {
            var viewModel = new QuestionsViewModel
            {
                Id = answers.Id,
                StudentAnswer = answers.StudentAnswer,
                CheckAnswer = answers.CheckAnswer,
                ValueAnswer = answers.ValueAnswer
            };
            return viewModel;
        }

        private void UpdateAnswer(Answers answers, QuestionsViewModel viewModel)
        {
            answers.Id = viewModel.Id;
            answers.StudentAnswer = viewModel.StudentAnswer;

        }
    }
}
