using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sait2022.Hubs;
using Microsoft.AspNetCore.SignalR;
using Sait2022.ViewModels.Employee;
using System.Security.Claims;

namespace Sait2022.Controllers
{   
    public class EmployeeController:Controller
    {
        private readonly SaitDbContext db;
        private long UserId { get; set; }
        public EmployeeController(SaitDbContext context)
        {
            db = context;

        }

        public IActionResult Chat()
        {
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;

            var empl = db.Users.Where(x => x.Id == UserId & x.Employee.EmployeesNavig.Id == x.Employee.TeacherId)
                               .Include(a => a.Employee).Include(e => e.Employee.EmployeesNavig);

            ViewBag.Admin = db.Users.Where(x => x.Id == UserId);

            return View(await empl.OrderBy(x => x.Id).ToListAsync());
        }

        public async Task<IActionResult> Index2()
        {
            UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;

            return View(await db.Employees.Where(x => x.TeacherId==UserId).OrderBy(x => x.Id).ToListAsync());
        }
        public async Task<IActionResult> Index3()
        {
            var userId = long.Parse(this.User.FindFirstValue(ClaimTypes.NameIdentifier));

            return View(await db.Employees.Where(x => x.Id != userId).OrderBy(x => x.Id).ToListAsync());
        }

    }
}
