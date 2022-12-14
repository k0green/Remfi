using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using System.Text.RegularExpressions;

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
                            if(user.RoleId==1)
                            {
                                return RedirectToAction("DisplayAllFilmForAdmin", "Film");
                            }
                            return RedirectToAction("FindFilm", "Film");
                        }
                    }
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
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
                Regex regex = new Regex(@"(\w*)<(\w*)");
                MatchCollection matchesName = regex.Matches(model.Name);
                MatchCollection matchesPassword = regex.Matches(model.Password);
                if (matchesName.Count == 0 && matchesPassword.Count == 0)
                {
                    User user = await _userCrudlService.GetUserByLogin(model.Login);
                    if (user == null)
                    {
                        //_userCrudlService.CreateUser(model.Name, model.Login, model.Password, user.RoleId);
                        _userCrudlService.CreateUser(model);

                        await Authenticate(model.Login); // аутентификация

                        return RedirectToAction("Login", "Account");
                    }
                    else
                        ModelState.AddModelError("", "Некорректные логин и(или) пароль");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            ModelState.AddModelError("", "Некорректные логин и(или) пароль");
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
            Response.Cookies.Delete("UserId");
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("FindFilm", "Film");
        }

        public async Task<IActionResult> Profile()
        {
            var user = await _userCrudlService.GetUser(int.Parse(Request.Cookies["UserId"]));
            return View(user);
        }
    }
}