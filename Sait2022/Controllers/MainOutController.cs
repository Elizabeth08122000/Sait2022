using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace Sait2022.Controllers
{
    public class MainOutController:Controller
    {
        private readonly SaitDbContext db;
        private readonly IEnumerable<MainOut> mainOuts;
        IEnumerable<Questions> questions;
        public MainOutController(SaitDbContext context)
        {
            db = context;
            questions = db.Questions.OrderBy(x => x.Id);
            mainOuts = db.MainOuts;
        }

        [HttpGet]
        public IActionResult AnswerStudGet()
        {
            ViewBag.MainOut = mainOuts;
            return View("MainOut");
        }
    }
}
