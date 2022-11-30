using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetflixMVC.Interfaces;
using NetflixMVC.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace NetflixMVC.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {
        private readonly IFilmCrudlService _crudlFilmService;
        private readonly IUserFilmCrudlService _userFilmCrudlService;
        private readonly ISeriesCrudlService _seriesCrudlService;
        private readonly IUserFilmDetailsCrudlService _userFilmDetailsCrudlService;

        public FilmController(IFilmCrudlService crudlFilmService,
            ISeriesCrudlService seriesCrudlService,
            IUserFilmDetailsCrudlService userFilmDetailsCrudlService, IUserFilmCrudlService userFilmCrudlService)
        {
            _crudlFilmService = crudlFilmService;
            _seriesCrudlService = seriesCrudlService;
            _userFilmDetailsCrudlService = userFilmDetailsCrudlService;
            _userFilmCrudlService = userFilmCrudlService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DisplayAllFilm()
        {
            return View(await _crudlFilmService.GetAllFilm());
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllFilmForOneUser()
        {
            var film = _userFilmCrudlService.GetAllFilmForUser(int.Parse(Request.Cookies["UserId"]));
            return View(film);

        }

        public async Task<IActionResult> AdditionalInformation(int? filmId)
        {
            var film = await _crudlFilmService.GetFilm(filmId);
            var mark = await _userFilmDetailsCrudlService.GetMark(filmId, int.Parse(Request.Cookies["UserId"]));
            var comment = await _userFilmDetailsCrudlService.GetComment(filmId, int.Parse(Request.Cookies["UserId"]));
            var userFilm = await _userFilmCrudlService.GetAmountOfView(filmId, int.Parse(Request.Cookies["UserId"]));
            Response.Cookies.Append("FilmId", $"{film.Id}");
            ViewBag.CheckSeries = film.CheckSeries;
            ViewBag.Mark = mark;
            ViewBag.Comment = comment;
            ViewBag.UserFilm = userFilm;
            Response.Cookies.Append("FilmIdForCreateSeries", $"{filmId}");

            return View(film);
        }
        public async Task<IActionResult> GetFavorite()
        {
            var film = new List<Film>();
            var userFilm = await _userFilmCrudlService.GetToFavorite(int.Parse(Request.Cookies["UserId"]));
            foreach(var item in userFilm)
            {
                film.Add(await _crudlFilmService.GetFilm(item.FilmId));
            }
            return View(film);
        }

        [HttpGet]
        public IActionResult CreateFilm()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateFilm(Film film)
        {
            await _crudlFilmService.CreateFilm(film);
            return Redirect($"~/UserFilm/CreateUserFilmConnection?filmName={film.Name}&filmDate={film.ReleaseData}");
        }

        [HttpGet]
        public async Task<IActionResult> EditFilm(int? id)
        {
            if (id != null)
            {
                return View(await _crudlFilmService.UpdateFilm(id));
            }
            return NotFound();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FindFilm()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> FindFilmPost(string name)
        {
            var film = await _crudlFilmService.FindFilm(name);
            if (film == null)
            {
                return RedirectToAction("FilmNotFound");
            }

            if (Request.Cookies["UserId"] != null)
            {
                var mark = await _userFilmDetailsCrudlService.GetMark(film.Id, int.Parse(Request.Cookies["UserId"]));
                var comment = await _userFilmDetailsCrudlService.GetComment(film.Id, int.Parse(Request.Cookies["UserId"]));
                var userFilm = await _userFilmCrudlService.GetAmountOfView(film.Id, int.Parse(Request.Cookies["UserId"]));
                Response.Cookies.Append("FilmId", $"{film.Id}");
                ViewBag.CheckSeries = film.CheckSeries;
                ViewBag.Mark = mark;
                ViewBag.Comment = comment;
                ViewBag.UserFilm = userFilm;
            }
            else
            {
                ViewBag.CheckSeries = film.CheckSeries;
                ViewBag.Mark = 0;
                ViewBag.Comment = "please auutoriixze";
                ViewBag.UserFilm = 0;

            }

            Response.Cookies.Append("FilmIdForCreateSeries", $"{film.Id}");
            return View(film);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FilmNotFound(string name)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditFilm(Film film)
        {
            await _crudlFilmService.UpdateFilm(film);
            return RedirectToAction("DisplayAllFilmForOneUser");
        }

        [HttpGet]
        [ActionName("DeleteFilm")]
        public async Task<IActionResult> ConfirmDeleteFilm(int? id)
        {
            ViewData["FilmId"] = $"id={id}";
            if (id != null)
            {
                return View(await _crudlFilmService.ConfirmDeleteFilm(id));
            }
            return NotFound();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFilm(int? id)
        {
            if (id != null)
            {
                await _crudlFilmService.DeleteFilm(id);
                return RedirectToAction("DisplayAllFilm");
            }
            return NotFound();
        }
    }
}
