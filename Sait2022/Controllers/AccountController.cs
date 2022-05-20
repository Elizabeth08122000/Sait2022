using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Sait2022.Domain.DB;
using Sait2022.ViewModels.Account;
using Sait2022.Security;
using Sait2022.Domain.Model;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Sait2022.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Users> _userManager;
        private readonly SaitDbContext _saitDbContext;

        /// <summary>
        /// Конструктор класса <see cref="AccountController"/>
        /// </summary>
        /// <param name="userManager">Менеджер пользователей</param>
        /// <param name="_saitDbContext">Контекст базы данных</param>
        public AccountController(UserManager<Users> userManager, SaitDbContext saitDbContext)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _saitDbContext = saitDbContext ?? throw new ArgumentNullException(nameof(_saitDbContext));
        }

        public IActionResult Index()
        {
            return View(_userManager.Users.Skip(1).ToList());
        }

        /// <summary>
        /// Форма входа в систему
        /// </summary>
        /// <param name="returnUrl">Путь перехода после авторизации</param>
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            // Очистить существующие куки для корректного логина
         //   await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        /// <summary>
        /// Авторизация в системе
        /// </summary>
        /// <param name="signInManager">Менеджер авторизации</param>
        /// <param name="model">Входные данные с формы</param>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromServices] SignInManager<Users> signInManager, LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.Login).Result;

                  if (user == null)
                  {
                      ModelState.AddModelError(string.Empty, "Проверьте имя пользователя и пароль");
                      return View(model);
                  }

                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                    await Authenticate(model.Login); // аутентификация
                    return RedirectToAction("Index", "Home");

                if (result.IsLockedOut)
                    {
                        ModelState.AddModelError(string.Empty, "Ваш аккаунт заблокирован. Подождите минуту для разблокировки или обратитесь к администратору.");
                        return View(model);
                    }

      
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return View(model);
            }

            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }


        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        [HttpGet]
        public IActionResult RegistrationNewUser()
        {
            return View();
        }

        /// <summary>
        /// Регистрация нового пользователя
        /// </summary>
        /// <param name="model">Данные о новом пользователе</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrationNewUserAsync(NewUserViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            if (_userManager.Users.Any(x => x.UserName.ToLower() == model.UserName.ToLower()))
                ModelState.AddModelError("Username", "Пользователь с таким именем пользователя уже существует!");

            if (_userManager.Users.Any(x => x.Email.ToLower() == model.Email.ToLower()))
                ModelState.AddModelError("Email", "Такой email уже используеся в системе");

            if (ModelState.ErrorCount > 0)
                return View(model);

            var profile = new Employee
            {
                FirstName = model.FirstName,
                Surname = model.Surname,
                Patronym = model.Patronym,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                TeacherId = model.TeacherId,
                IsAdministrator = model.IsAdministrator,
                IsTeacher = model.IsTeacher
            };

            var user = new Users
            {
                UserName = model.UserName,
                Email = model.Email,
                Employee = profile
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                AddErrors(result);
                return View(model);
            }

            if (model.IsAdministrator)
            {
                await _userManager.AddToRoleAsync(user, SecurityConstants.AdminRole);
            }
            if (model.IsTeacher)
            {
                await _userManager.AddToRoleAsync(user, SecurityConstants.TeacherRole);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, SecurityConstants.StudentRole);
            }
            _saitDbContext.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Выход из системы
        /// </summary>
        /// <param name="signInManager">Менеджер авторизации</param>
        [HttpGet]
        public async Task<IActionResult> Logout([FromServices] SignInManager<Users> signInManager)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await signInManager.SignOutAsync();


            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> SetLockout(long id)
        {
            Users user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            LockoutUsersViewModel model = new LockoutUsersViewModel { Id = user.Id, Email = user.Email, TimeLockout = user.LockoutEnd };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SetLockout(LockoutUsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    if (user.LockoutEnd == null)
                    {
                        user.LockoutEnd = model.TimeLockout;
                        await _saitDbContext.SaveChangesAsync();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Пользователь уже заблокирован");
                    }
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователь не найден");
            }
            return View(model);
        }

        /// <summary>
        /// Возвращение страницы в случае блокировки пользователя
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        /// <summary>
        /// Подтверждение сброса пароля
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        /// <summary>
        /// Страница запрета доступа
        /// </summary>
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        public async Task<IActionResult> ChangePassword(long id)
        {
            Users user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            ChangePasswordViewModel model = new ChangePasswordViewModel { Id = user.Id, Email = user.Email };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = await _userManager.FindByIdAsync(model.Id.ToString());
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<Users>)) as IPasswordValidator<Users>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<Users>)) as IPasswordHasher<Users>;

                    IdentityResult result =
                        await _passwordValidator.ValidateAsync(_userManager, user, model.Password);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                        await _userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Пользователь не найден");
                }
            }
            return View(model);
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
