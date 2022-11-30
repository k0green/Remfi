using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using NetflixMVC.Services;

namespace NetflixMVC.Controllers
{
    [Authorize]
    public class UserFilmController : Controller
    {
        private readonly IUserFilmCrudlService _crudlUserFilmService;
        private readonly IUserFilmDetailsCrudlService _crudlUserFilmDetailsService;

        public UserFilmController(IUserFilmCrudlService crudlUserFilmService, IUserFilmDetailsCrudlService crudlUserFilmDetailsService)
        {
            _crudlUserFilmService = crudlUserFilmService;
            _crudlUserFilmDetailsService = crudlUserFilmDetailsService;
        }


        [HttpGet]
        public async Task<IActionResult> DisplayAllFilmForOneUser()
        {
            return View(_crudlUserFilmService.GetAllFilmForUser(int.Parse(Request.Cookies["UserId"])));
        }

        public async Task<IActionResult> CreateUserFilmConnection(string filmName, string filmDate)
        {
            _crudlUserFilmService.CreateUserFilmConnection(int.Parse(Request.Cookies["UserId"]), filmName, filmDate);
            return RedirectToAction("DisplayAllFilmForOneUser", "Film");
        }
        public async Task<IActionResult> AddAmountOfView(int id)
        {
            await _crudlUserFilmService.AddAmountOfView(id, int.Parse(Request.Cookies["UserId"]));
            return RedirectToAction("DisplayAllFilmForOneUser", "Film");
        }

        public async Task<IActionResult> AddToFavorite(int filmId, bool favorite)
        {
            await _crudlUserFilmService.AddToFavorite(filmId, int.Parse(Request.Cookies["UserId"]), favorite);
            return RedirectToAction("DisplayAllFilmForOneUser", "Film");
        }
    }
}
