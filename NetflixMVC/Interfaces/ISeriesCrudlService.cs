using NetflixMVC.Entities;
using NetflixMVC.Models;

namespace NetflixMVC.Interfaces
{
    public interface ISeriesCrudlService
    {
        public Task<List<SeriesDisplayModel>> GetAllSeriesForOneFilm(int? filmId, int userId);
        public Task<List<Series>> GetAllSeriesForOneFilmForAdmin(int? filmId);
        public Series GetOneSeries(int? seriesId);
        public Task CreateSeries(Series series);
        public Task<Series> UpdateSeries(int? id);
        public Task UpdateSeries(Series series);
        public Task DeleteSeries(int? id);
        public Task<Series> ConfirmDeleteSeries(int? id);
        //public Task AddMark(int? seriesId, float mark);
        //public Task AddComments(int? seriesId, string description);
    }
}
