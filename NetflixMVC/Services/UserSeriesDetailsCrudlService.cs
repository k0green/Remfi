using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using NetflixMVC.Models;

namespace NetflixMVC.Services
{
    public class UserSeriesDetailsCrudlService : IUserSeriesDetailsCrudlService
    {
        private readonly IDbContext _dbContext;

        public UserSeriesDetailsCrudlService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddMark(int? seriesId, int? userId, float mark)
        {
            Usersseriesdetail usersseriesDetail = new();
            var usersSeriesDetailChecked = _dbContext.Usersseriesdetails.FirstOrDefault(p => p.UserId == userId && p.SeriesId == seriesId);
            usersseriesDetail.Mark = mark;
            usersseriesDetail.UserId = userId;
            usersseriesDetail.SeriesId = seriesId;
            if (usersSeriesDetailChecked == null)
            {
                _dbContext.Usersseriesdetails.Add(usersseriesDetail);
            }
            else
            {
                usersSeriesDetailChecked.Mark = mark;
            }
            _dbContext.SaveChanges();

        }
        public async Task AddComments(int? seriesId, int? userId, string comments)
        {
            Usersseriesdetail usersseriesDetail = new();
            var usersSeriesDetailChecked = _dbContext.Usersseriesdetails.FirstOrDefault(p => p.UserId == userId && p.SeriesId == seriesId);
            usersseriesDetail.Comment = comments;
            usersseriesDetail.UserId = userId;
            usersseriesDetail.SeriesId = seriesId;
            if (usersSeriesDetailChecked == null)
            {
                _dbContext.Usersseriesdetails.Add(usersseriesDetail);
            }
            else
            {
                usersSeriesDetailChecked.Comment = comments;
            }
            _dbContext.SaveChanges();
        }
        public async Task<float> GetMark(int? seriesId, int? userId)
        {
            var usersfilmsdetail = _dbContext.Usersseriesdetails.FirstOrDefault(p => p.UserId == userId && p.SeriesId == seriesId);
            if (usersfilmsdetail == null)
            {
                return 0;
            }
            else
            {
                var mark = usersfilmsdetail.Mark;
                return mark;
            }
        }

        public async Task<string> GetComment(int? seriesId, int? userId)
        {
            var comment = _dbContext.Usersseriesdetails.FirstOrDefault(p => p.UserId == userId && p.SeriesId == seriesId);
            if (comment == null)
            {
                return "empty";
            }
            else
            {
                return comment.Comment;
            }
        }

        public Task EditMark(int? seriesId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task EditComment(int? seriesId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMark(int? seriesId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComment(int? seriesId, int? userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<SeriesDisplayModel>> GetAllSeries(int? filmId, int userId)
        {
            var usersSeries = _dbContext.Usersseriesdetails.Select(x => new SeriesDisplayModel
            {
                Id = x.Series.Id,
                Season = x.Series.Season,
                NumberOfSeries = x.Series.NumberOfSeries,
                SeriesId = x.Series.Id,
                Comment = x.Comment,
                Mark = x.Mark,
                FilmId = x.Series.FilmId,
                NumberOfView = x.NumberOfView,
                UserId = (int)x.UserId,
            }).Where(p => p.FilmId == filmId).ToList();

            return usersSeries;
        }

        public async Task CreateSeriesDetail(int? userId, int seriesSeason, int seriesSeries, int filmId)
        {
            var series = _dbContext.Series.FirstOrDefault(p => p.Season == seriesSeason && p.NumberOfSeries == seriesSeries && p.FilmId == filmId);
            var userSeriesDetail = new Usersseriesdetail();
            userSeriesDetail.UserId = userId;
            userSeriesDetail.SeriesId = series.Id;
            userSeriesDetail.NumberOfView = 0;
            userSeriesDetail.Mark = 0;
            userSeriesDetail.Comment = "empty";
            _dbContext.Usersseriesdetails.Add(userSeriesDetail);
            _dbContext.SaveChanges();
        }

        public async Task AddAmountOfView(int seriesId, int userId)
        {
            var series = _dbContext.Usersseriesdetails.FirstOrDefault(p => p.SeriesId == seriesId && p.UserId == userId);
            series.NumberOfView++;
            _dbContext.SaveChanges();
        }

        public async Task<int> GetAmountOfView(int? seriesId, int? userId)
        {
            var film = _dbContext.Usersseriesdetails.FirstOrDefault(p => p.SeriesId == seriesId && p.UserId == userId);
            int amountOfView = (int)film.NumberOfView;
            return amountOfView;
        }
    }
}
