using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetflixMVC.Interfaces;

namespace NetflixMVC.Controllers
{
    [Authorize]
    public class UserFilmDetailsController : Controller
    {
        private readonly IUserFilmDetailsCrudlService _crudlUserFilmDetauilsService;
        private readonly IFilmCrudlService _crudlFilmService;


        public UserFilmDetailsController(IUserFilmDetailsCrudlService crudlUserFilmDetauilsService, IFilmCrudlService crudlFilmService)
        {
            _crudlUserFilmDetauilsService = crudlUserFilmDetauilsService;
            _crudlFilmService = crudlFilmService;
        }

        [HttpGet]
        public async Task<IActionResult> AddFilmMark(int? filmId)
        {
            Response.Cookies.Append("filmIdFromAddFilmMark", $"{filmId}");
            return View(_crudlFilmService.GetFilm(filmId));
        }
        [HttpPost]
        public async Task<IActionResult> AddFilmMark(float mark)
        {
            await _crudlUserFilmDetauilsService.AddMark(int.Parse(Request.Cookies["filmIdFromAddFilmMark"]), int.Parse(Request.Cookies["userId"]), mark);
            return RedirectToAction("DisplayAllFilmForOneUser", "Film");
        }
        [HttpGet]
        public async Task<IActionResult> AddFilmComment(int? filmId)
        {
            Response.Cookies.Append("filmIdFromAddFilmComment", $"{filmId}");
            return View(_crudlFilmService.GetFilm(filmId));
        }
        [HttpPost]
        public async Task<IActionResult> AddFilmComment(string comment)
        {
            await _crudlUserFilmDetauilsService.AddComment(int.Parse(Request.Cookies["filmIdFromAddFilmComment"]), int.Parse(Request.Cookies["userId"]), comment);
            return RedirectToAction("DisplayAllFilmForOneUser", "Film");
        }
    }
}
