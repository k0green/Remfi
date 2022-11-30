using Microsoft.EntityFrameworkCore;
using NetflixMVC.Models;

namespace NetflixMVC.Interfaces
{
    public interface IUserSeriesDetailsCrudlService
    {
        public Task AddMark(int? seriesId, int? userId, float mark);
        public Task AddComments(int? seriesId, int? userId, string comments);
        public Task<float> GetMark(int? seriesId, int? userId);
        public Task<string> GetComment(int? seriesId, int? userId);
        public Task EditMark(int? seriesId, int? userId);
        public Task EditComment(int? seriesId, int? userId);
        public Task DeleteMark(int? seriesId, int? userId);
        public Task DeleteComment(int? seriesId, int? userId);
        public Task<List<SeriesDisplayModel>> GetAllSeries(int? filmId, int userId);
        public Task CreateSeriesDetail(int? userId, int seriesSeason, int seriesSeries, int filmId);
        public Task AddAmountOfView(int seriesId, int userId);
        public Task<int> GetAmountOfView(int? seriesId, int? userId);
    }
}
