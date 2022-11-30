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

        public SeriesController(ISeriesCrudlService crudlSeriesService, IUserSeriesDetailsCrudlService userSeriesDetailsService)
        {
            _crudlSeriesService = crudlSeriesService;
            _userSeriesDetailsService = userSeriesDetailsService;
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

        [HttpDelete]
        public async Task<IActionResult> DeleteSeries(int? id)
        {
            if (id != null)
            {
                await _crudlSeriesService.DeleteSeries(id);
                return RedirectToAction("DisplayAllSeriesByFilmId");
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
