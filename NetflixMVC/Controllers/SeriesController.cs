using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;

namespace NetflixMVC.Controllers
{
    [Authorize]
    public class SeriesController : Controller
    {
        private readonly ISeriesCrudlService _crudlSeriesService;
        private readonly IUserSeriesDetailsCrudlService _userSeriesDetailsService;
        private readonly IUserCrudlService _userCrudlService;

        public SeriesController(ISeriesCrudlService crudlSeriesService, IUserSeriesDetailsCrudlService userSeriesDetailsService, IUserCrudlService userCrudlService)
        {
            _crudlSeriesService = crudlSeriesService;
            _userSeriesDetailsService = userSeriesDetailsService;
            _userCrudlService = userCrudlService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> DisplayAllSeriesByFilmId(int? filmId)////////////////////
        {
            var series = await _crudlSeriesService.GetAllSeriesForOneFilm(int.Parse(Request.Cookies["FilmIdForCreateSeries"]), int.Parse(Request.Cookies["UserId"]));
            series = series.Where(p => p.FilmId == int.Parse(Request.Cookies["FilmIdForCreateSeries"])).ToList();
            return View(series);
        }

        [HttpGet]
        public async Task<IActionResult> DisplayAllSeriesByFilmIdForAdmin(int? filmId)////////////////////
        {
            var series = await _crudlSeriesService.GetAllSeriesForOneFilmForAdmin(int.Parse(Request.Cookies["FilmIdForCreateSeries"]));
            //series = series.Where(p => p.FilmId == int.Parse(Request.Cookies["FilmIdForCreateSeries"])).ToList();
            return View(series);
        }

        [HttpGet]
        public IActionResult CreateSeries()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeries(Series series)
        {
            series.FilmId = int.Parse(Request.Cookies["FilmIdForCreateSeries"]);
            await _crudlSeriesService.CreateSeries(series);
            return Redirect($"~/UserSeriesDetails/CreateSeriesDetail?seriesSeason={series.Season}&seriesSeries={series.NumberOfSeries}");
        }

        [HttpPost]
        public async Task<IActionResult> CreateALargeNumbersOfSeries(int season, int firstSeries, int lastSeries)
        {
            var user = await _userCrudlService.GetUser(int.Parse(Request.Cookies["UserId"]));
            for(var i=firstSeries; i <= lastSeries; i++)
            {
                var series = new Series();
                series.FilmId = int.Parse(Request.Cookies["FilmIdForCreateSeries"]);
                series.NumberOfSeries = i;
                series.Season = season;
                await _crudlSeriesService.CreateSeries(series);
                if (user.RoleId == 2)
                {
                    await _userSeriesDetailsService.CreateSeriesDetail(int.Parse(Request.Cookies["UserId"]), season, i, int.Parse(Request.Cookies["FilmIdForCreateSeries"]));
                }
            }
            return RedirectToAction("DisplayAllSeriesByFilmIdForAdmin", "Series");
        }

        [HttpGet]
        public async Task<IActionResult> EditSeries(int? id)
        {
            if (id != null)
            {
                return View(await _crudlSeriesService.UpdateSeries(id));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> EditSeries(Series series)
        {
            await _crudlSeriesService.UpdateSeries(series);
            return Redirect($"~/Series/DisplayAllSeriesByFilmId?filmId={series.FilmId}");
        }

        [HttpGet]
        [ActionName("DeleteSeries")]
        public async Task<IActionResult> ConfirmDeleteSeries(int? id)
        {
            ViewData["SeriesId"] = $"id={id}";
            if (id != null)
            {
                return View(await _crudlSeriesService.ConfirmDeleteSeries(id));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSeries(int? id)
        {
            if (id != null)
            {
                await _crudlSeriesService.DeleteSeries(id);
                return RedirectToAction("DisplayAllSeriesByFilmIdForAdmin");
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> AddMark(int? seriesId)
        {
            Response.Cookies.Append("seriesIdFromAddMark", $"{seriesId}");
            return View(_crudlSeriesService.GetOneSeries(seriesId));
        }
    }
}
