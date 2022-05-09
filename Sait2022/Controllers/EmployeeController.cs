using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sait2022.Domain.DB;
using Sait2022.Domain.Model;
using Sait2022.ViewModels.Account;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignalRChat.Hubs;

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
            return RedirectToAction("");
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            UserId = db.Users.FirstOrDefault(x => x.Id == int.Parse(User.Identity.GetUserId())).EmployeeId;
            var employee = db.Employees.Where(x => x.Id == UserId).Join(db.Users,
                                            e => e.Id,
                                            u => u.EmployeeId,
                                            (e, u) => new
                                            {
                                                Name = e.FirstName,
                                                Surname = e.Surname,
                                                Patronym = e.Patronym,
                                                Phone = e.PhoneNumber,
                                                Address = e.Address,
                                                Email = u.Email,
                                                Login = u.UserName
                                            });

            List<Employee> listEmployee = new List<Employee>();
            List<Users> listUsers = new List<Users>();
            foreach (var empl in employee)
            {
                Users users = new Users();
                Employee employees = new Employee();
                employees.FirstName = empl.Name;
                employees.Surname = empl.Surname;
                employees.Patronym = empl.Patronym;
                employees.PhoneNumber = empl.Phone;
                employees.Address = empl.Address;
                users.Email = empl.Email;
                users.UserName = empl.Login;
                listEmployee.Add(employees);
                listUsers.Add(users);
                //listEmployee.AddRange((IEnumerable<Employee>)listUsers);
            }
            return View(listEmployee);
        }

        private IActionResult GetEmployee()
        {
            var employee = db.Employees.Where(x => x.Id == UserId).Join(db.Users,
                                            e => e.Id,
                                            u => u.EmployeeId,
                                            (e,u)=> new
                                            {
                                                Name = e.FirstName,
                                                Surname = e.Surname,
                                                Patronym = e.Patronym,
                                                Phone = e.PhoneNumber,
                                                Address = e.Address,
                                                Email = u.Email,
                                                Login = u.UserName
                                            });

            List<Employee> listEmployee = new List<Employee>();
            List<Users> listUsers = new List<Users>();
            foreach (var empl in employee)
            {
                Users users = new Users();
                Employee employees = new Employee();
                employees.FirstName = empl.Name;
                employees.Surname = empl.Surname;
                employees.Patronym = empl.Patronym;
                employees.PhoneNumber = empl.Phone;
                employees.Address = empl.Address;
                users.Email = empl.Email;
                users.UserName = empl.Login;
                listEmployee.Add(employees);
                listUsers.Add(users);
                listEmployee.AddRange((IEnumerable<Employee>)listUsers);
            }
            return View(listEmployee);
        }
    }
}
