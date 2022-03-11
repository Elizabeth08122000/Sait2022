using Microsoft.AspNetCore.Mvc;
using Sait2022.Domain.DB;
using System.Linq;

namespace Sait2022.Controllers
{
    public class MainOutController:Controller
    {
        private readonly SaitDbContext db;
        public MainOutController(SaitDbContext context)
        {
            db = context;
        }

        [HttpGet]
        public IActionResult AnswerStudGet()
        {
            return View("MainOut",db.MainOuts.ToList());
        }
    }
}
