using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;

namespace NetflixMVC.Controllers
{
    [Authorize]
    public class UserSeriesDetailsController : Controller
    {
        private readonly IUserSeriesDetailsCrudlService _crudlUserSeriesDetailsService;
        private readonly ISeriesCrudlService _crudlSeriesCrudlService;


        public UserSeriesDetailsController(IUserSeriesDetailsCrudlService crudlUserSeriesDetailsService, ISeriesCrudlService crudlSeriesService)
        {
            _crudlUserSeriesDetailsService = crudlUserSeriesDetailsService;
            _crudlSeriesCrudlService = crudlSeriesService;
        }

        public async Task<IActionResult> CreateUserSeriesConnection()
        {
            await _crudlUserSeriesDetailsService.CreateUserSeriesConnection(int.Parse(Request.Cookies["UserId"]), int.Parse(Request.Cookies["FilmIdForCreateSeries"]));
            return Redirect($"~/Series/DisplayAllSeriesByFilmId?filmId={int.Parse(Request.Cookies["FilmIdForCreateSeries"])}");
        }

        public async Task<IActionResult> DeleteUserSeriesConnection(int seriesId)
        {
            await _crudlUserSeriesDetailsService.DeleteUserSeriesConnection(int.Parse(Request.Cookies["UserId"]), seriesId);
            return RedirectToAction("DisplayAllFilmForOneUser", "Film");
        }

        [HttpGet]
        public async Task<IActionResult> AddSeriesMark(int? seriesId)
        {
            Response.Cookies.Append("seriesIdFromAddFilmMark", $"{seriesId}");
            return View(_crudlSeriesCrudlService.GetOneSeries(seriesId));
        }

        [HttpPost]
        public async Task<IActionResult> AddSeriesMark(float mark)
        {
            await _crudlUserSeriesDetailsService.AddMark(int.Parse(Request.Cookies["seriesIdFromAddFilmMark"]), int.Parse(Request.Cookies["userId"]), mark);
            return RedirectToAction("DisplayAllSeriesByFilmId", "Series");
        }

        [HttpGet]
        public async Task<IActionResult> AddSeriesComment(int? seriesId)
        {
            Response.Cookies.Append("seriesIdFromAddFilmComment", $"{seriesId}");
            return View(_crudlSeriesCrudlService.GetOneSeries(seriesId));
        }


        [HttpPost]
        public async Task<IActionResult> AddSeriesComment(string comment)
        {
            await _crudlUserSeriesDetailsService.AddComments(int.Parse(Request.Cookies["seriesIdFromAddFilmComment"]), int.Parse(Request.Cookies["userId"]), comment);
            return RedirectToAction("DisplayAllSeriesByFilmId", "Series");
        }



        public async Task<IActionResult> CreateSeriesDetail(int seriesSeason, int seriesSeries)
        {
            _crudlUserSeriesDetailsService.CreateSeriesDetail(int.Parse(Request.Cookies["UserId"]), seriesSeason, seriesSeries, int.Parse(Request.Cookies["FilmIdForCreateSeries"]));
            return RedirectToAction("DisplayAllSeriesByFilmId", "Series");
        }
        public async Task<IActionResult> AddAmountOfView(int seriesId)
        {
            await _crudlUserSeriesDetailsService.AddAmountOfView(seriesId, int.Parse(Request.Cookies["UserId"]));
            return RedirectToAction("DisplayAllSeriesByFilmId", "Series");
        }
    }
}
