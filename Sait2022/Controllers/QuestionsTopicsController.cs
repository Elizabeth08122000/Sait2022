using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sait2022.Controllers
{
    [Controller]
    public class QuestionsTopicsController:Controller
    {
        private readonly SaitDbContext db;

        public QuestionsTopicsController(SaitDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult QuestionsTopics()
        {
            return View(db.QuestionsTopics.ToList());
        }
    }
}
