using Microsoft.EntityFrameworkCore;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using NetflixMVC.Models;

namespace NetflixMVC.Services
{
    public class SeriesCrudlService : ISeriesCrudlService
    {
        private readonly IDbContext _dbContext;
        private readonly IUserSeriesDetailsCrudlService _userSeriesDetailsCrudlService;

        public SeriesCrudlService(IDbContext dbContext, IUserSeriesDetailsCrudlService userSeriesDetailsCrudlService)
        {
            _dbContext = dbContext;
            _userSeriesDetailsCrudlService = userSeriesDetailsCrudlService;
        }

        //public async Task AddComments(int? seriesId, string description)
        //{
        //    Series series = GetOneSeries(seriesId);
        //    series.Comments = description;
        //    _dbContext.SaveChanges();
        //}

        //public async Task AddMark(int? seriesId, float mark)
        //{
        //    Series series = GetOneSeries(seriesId);
        //    series.Mark = mark;
        //    _dbContext.SaveChanges();
        //}

        public async Task<Series> ConfirmDeleteSeries(int? id)
        {
            var series = GetOneSeries(id);
            return series ?? throw new Exception();
        }

        public async Task CreateSeries(Series series)
        {
            _dbContext.Series.Add(series);
            _dbContext.SaveChanges();
        }

        public async Task DeleteSeries(int? id)
        {
            var series = GetOneSeries(id);
            if (series != null)
            {
                _dbContext.Series.Remove(series);
                _dbContext.SaveChanges();
            }
        }

        public async Task<List<SeriesDisplayModel>> GetAllSeriesForOneFilm(int? filmId, int userId)
        {
            var listSeries = await _userSeriesDetailsCrudlService.GetAllSeries(filmId, userId);

            return listSeries;
        }

        public Series GetOneSeries(int? seriesId)
        {
           Series series = _dbContext.Series.FirstOrDefault(s => s.Id == seriesId) ?? throw new Exception();
            return series;
        }

        public async Task<Series> UpdateSeries(int? id)
        {
            var series =await _dbContext.Series.FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception();
            return series;
        }

        public async Task UpdateSeries(Series series)
        {
            _dbContext.Series.Update(series);
            _dbContext.SaveChanges();
        }
    }
}
