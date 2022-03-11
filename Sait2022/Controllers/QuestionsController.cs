using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using System;
using System.Linq;

namespace Sait2022.Controllers
{
    public class QuestionsController:Controller
    {
        private readonly SaitDbContext db;

        public QuestionsController(SaitDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult QuestionsGet()
        { 
            return View("Questions", db.Questions.OrderBy(x=>x.Id).ToList());
        }
    }
}
