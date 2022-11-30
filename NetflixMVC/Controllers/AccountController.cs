using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;

namespace NetflixMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserCrudlService _userCrudlService;
        public AccountController(IUserCrudlService crudlUserService)
        {
            _userCrudlService = crudlUserService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userCrudlService.GetUserByLogin(model.Login);
                var dbpwd = user.Password;
                var mpwd = model.Password;
                if (IRegistrationService.VerifyHashedPassword(dbpwd, mpwd))
                {
                    if (user != null)
                    {
                        await Authenticate(model.Login); // аутентификация
                        Response.Cookies.Append("UserId", $"{user.Id}");
                        return RedirectToAction("FindFilm", "Film");
                    }
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userCrudlService.GetUserByLogin(model.Login);
                if (user == null)
                {
                    _userCrudlService.CreateUser(model.Name, model.Login, model.Password);

                    await Authenticate(model.Login); // аутентификация

                    return RedirectToAction("Login", "Account");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}