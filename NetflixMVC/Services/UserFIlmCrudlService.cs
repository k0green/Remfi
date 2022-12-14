
using Microsoft.EntityFrameworkCore;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using NetflixMVC.Models;

namespace NetflixMVC.Services
{
    public class UserFIlmCrudlService : IUserFilmCrudlService
    {
        private readonly IDbContext _dbContext;

        public UserFIlmCrudlService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public List<FilmUserDisplayModel> GetAllFilmForUser(int? userId)
        {
            var usersFilm = _dbContext.Userfilms.Select(x => new FilmUserDisplayModel
            {
                Id = x.Film.Id,
                Name = x.Film.Name,
                Genre = x.Film.Genre,
                DurationTime = x.Film.DurationTime,
                Description = x.Film.Description,
                ReleaseData = x.Film.ReleaseData,
                CheckSeries = x.Film.CheckSeries,
                Country = x.Film.Country,
                Producer = x.Film.Producer,
                Link = x.Film.Link,
                Rating = x.Film.Rating,
                UserId = x.UserId,
                FilmId = x.Film.Id,
                Favorites = x.Favorites,
                NumberOfView = x.NumberOfView
            }).ToList();

            var uFilm = usersFilm.Where(x => x.UserId == userId).ToList();

            //var usersFilm = usersFilmAll.Where(x => x.UserId == userId).ToList();
            //var usersFilm = _dbContext.Userfilms.Include(x => x.UserId == userId).ToList();
            return uFilm;
        }

        public FilmUserDisplayModel GetFindFilm(string name)
        {
            //var usersFilm = _dbContext.Userfilms.Where(x => x.UserId == userId).ToList();

            var usersFilm = _dbContext.Userfilms.Select(x => new FilmUserDisplayModel
            {
                Id = x.Film.Id,
                Name = x.Film.Name,
                Genre = x.Film.Genre,
                DurationTime = x.Film.DurationTime,
                Description = x.Film.Description,
                ReleaseData = x.Film.ReleaseData,
                CheckSeries = x.Film.CheckSeries,
                Country = x.Film.Country,
                Producer = x.Film.Producer,
                Link = x.Film.Link,
                Rating = x.Film.Rating,
                UserId = x.UserId,
                FilmId = x.Film.Id,
                Favorites = x.Favorites,
                NumberOfView = x.NumberOfView
            }).ToList();

            var uFilm = usersFilm.FirstOrDefault(x => x.Name == name);
            return uFilm;
        }

        public async Task<Userfilm> GetOneFilmForUser(int? userId, int? filmId)
        {
            var usersFilm = _dbContext.Userfilms.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);
            return usersFilm;
        }
        public async Task CreateUserFilmConnection(int? userId, string filmName, string filmDate)
        {
            var film = _dbContext.Films.FirstOrDefault(p => p.Name == filmName && p.ReleaseData == filmDate);
            var uFilm = _dbContext.Userfilms.FirstOrDefault(p => p.UserId == userId && p.FilmId == film.Id);
            if (uFilm == null)
            {
                var userFilm = new Userfilm();
                userFilm.UserId = userId;
                userFilm.FilmId = film.Id;
                userFilm.NumberOfView = 0;
                userFilm.Favorites = false;
                _dbContext.Userfilms.Add(userFilm);
                var userFilmDetails = new Usersfilmsdetail();
                userFilmDetails.UserId = userId;
                userFilmDetails.FilmId = film.Id;
                userFilmDetails.Comment = "empty";
                userFilmDetails.Favorites = false;
                userFilmDetails.Mark = 0;
                _dbContext.Usersfilmsdetails.Add(userFilmDetails);
                _dbContext.SaveChanges();
            }
        }

        public async Task DeleteUserFilmConnection(int? userId, int filmId)
        {
            var userFilm = _dbContext.Userfilms.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);

            if (userFilm != null)
            {
                _dbContext.Userfilms.Remove(userFilm);
                _dbContext.SaveChanges();
            }
        }

        public async Task AddAmountOfView(int? filmId, int userId)
        {
            var film = _dbContext.Userfilms.FirstOrDefault(p => p.FilmId == filmId && p.UserId == userId);
            film.NumberOfView++;
            _dbContext.SaveChanges();
        }

        public async Task<int> GetAmountOfView(int? filmId, int? userId)
        {
            int amountOfView = 0;
            var film = _dbContext.Userfilms.FirstOrDefault(p => p.FilmId == filmId && p.UserId == userId);
            if (film != null)
            {
                amountOfView = film.NumberOfView;
            }
            return amountOfView;
        }
        public async Task AddToFavorite(int? filmId, int? userId, bool favorite)
        {
            var usersFilms = _dbContext.Userfilms.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);
            usersFilms.Favorites = favorite;
            _dbContext.SaveChanges();
        }
        public async Task<List<Userfilm>> GetToFavorite(int? userId)
        {
            var film = _dbContext.Userfilms.Where(p=>p.UserId == userId && p.Favorites).ToList();
            return film;
        }

        public async Task<bool> GetToFavoriteValue(int? userId, int filmId)
        {
            var film = _dbContext.Userfilms.FirstOrDefault(p => p.UserId == userId && p.FilmId==filmId);
            var value = film.Favorites;
            return value;
        }
    }
}
