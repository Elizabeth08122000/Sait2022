using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sait2022.Controllers
{
    public class QuestionsController:Controller
    {
        private readonly SaitDbContext _saitDbContext;
        public QuestionsController(SaitDbContext saitDbContext)
        {
            _saitDbContext = saitDbContext ?? throw new ArgumentNullException(nameof(saitDbContext));
        }

        [HttpGet("Questions/{RangsId}")]
        public IActionResult QuestionsGet(long qustId)
        {
           var questions = _saitDbContext.Questions
                .Include(r => r.Rangs)
             //   .Include(m => m.Main_out)
                .Include(a => a.Answers)
                .Include(qt => qt.QuestionsTopic)
                .Where(x => x.Id == qustId)
                .Select(x => new QuestionsViewModel
                {
                    QuestionId = x.Id,
                    ValueQuest = x.ValueQuest,
                    Rangs = (ICollection<Rangs>)x.Rangs,
                    Answers = (ICollection<Answers>)x.Answers
                }).FirstOrDefault();
            return View("Questions",questions);
        }
/*
        [HttpPost]
        public IActionResult AnswerSave(SaveAnswersViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            Questions question = null;

            foreach(Questions q in _saitDbContext.Questions)
            {
                if(q.Id == model.QuestionId)
                {
                    question = q;
                }
            }

            var answer = new SaveAnswersViewModel
            {
                ValueAnswer = model.ValueAnswer,
                QuestionId = model.QuestionId,
                CheckAnswer = model.CheckAnswer
            };

            _saitDbContext.Add(answer);
            return View("Questions");
        }*/
    }
}
