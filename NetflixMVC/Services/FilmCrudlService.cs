using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using NetflixMVC.Models;

namespace NetflixMVC.Services
{
    public class FilmCrudlService : IFilmCrudlService
    {
        private readonly IDbContext _dbContext;
        private readonly IUserFilmCrudlService _userFilmCrudlService;

        public FilmCrudlService(IDbContext dbContext, IUserFilmCrudlService userFilmCrudlService)
        {
            _dbContext = dbContext;
            _userFilmCrudlService = userFilmCrudlService;
        }

        public async Task<List<FilmUserDisplayModel>> GetAllFilmForOneUser(int userId)//////////////////////////
        {
            var film = new List<Film>();
            var userFilm = _userFilmCrudlService.GetAllFilmForUser(userId);
            foreach (var uf in userFilm)
            {
                var filmAdd = await _dbContext.Films.FirstOrDefaultAsync(x => x.Id == uf.FilmId);
                film.Add(filmAdd);
            }
            return userFilm;

            //var film = new List<Film>();
            //var userFilm = _userFilmCrudlService.GetAllFilmForUser(userId);
            //foreach (var uf in userFilm)
            //{
            //    var filmAdd = _dbContext.Userfilms.Select(x => new FilmUserDisplayModel
            //    {
            //        Id = x.Film.Id,
            //        Name = x.Film.Name,
            //        Genre = x.Film.Genre,
            //        DurationTime = x.Film.DurationTime,
            //        Description = x.Film.Description,
            //        ReleaseData =x.Film.ReleaseData,
            //        CheckSeries = x.Film.CheckSeries,
            //        Country = x.Film.Country,
            //        Producer = x.Film.Producer,
            //        Link = x.Film.Link,
            //        Rating =x.Film.Rating,
            //        FilmId = x.Film.Id,
            //        UserId = userId,
            //        Favorites = x.Favorites,
            //        NumberOfView = x.NumberOfView
            //    }).Where(x => x.Id == uf.FilmId);

            //    var AllFilmInf = _dbContext.Userfilms.Include().Include(x => x.Film.Id && x.Film.Genre)

            //    film.Add(filmAdd);
            //}
            //return film;
        }

        public async Task CreateFilm(Film film)
        {
            var filmCheck = await _dbContext.Films.FirstOrDefaultAsync(p => p == film);
            if (filmCheck == null)
            {
                _dbContext.Films.Add(film);
                _dbContext.SaveChanges();
            }
        }

        public async Task<Film> FindFilm(string name)
        {
            var film = await GetFilmByName(name);

            //public List<FilmUserDisplayModel> GetAllFilmForUser(int? userId)
            //{
            //    //var usersFilm = _dbContext.Userfilms.Where(x => x.UserId == userId).ToList();

            //    var usersFilm = _dbContext.Userfilms.Select(x => new FilmUserDisplayModel
            //    {
            //        Id = x.Film.Id,
            //        Name = x.Film.Name,
            //        Genre = x.Film.Genre,
            //        DurationTime = x.Film.DurationTime,
            //        Description = x.Film.Description,
            //        ReleaseData = x.Film.ReleaseData,
            //        CheckSeries = x.Film.CheckSeries,
            //        Country = x.Film.Country,
            //        Producer = x.Film.Producer,
            //        Link = x.Film.Link,
            //        Rating = x.Film.Rating,
            //        UserId = x.UserId,
            //        FilmId = x.Film.Id,
            //        Favorites = x.Favorites,
            //        NumberOfView = x.NumberOfView
            //    }).ToList();

            //    var uFilm = usersFilm.Where(x => x.UserId == userId).ToList();

            //    //var usersFilm = usersFilmAll.Where(x => x.UserId == userId).ToList();
            //    //var usersFilm = _dbContext.Userfilms.Include(x => x.UserId == userId).ToList();
            //    return uFilm;
            //}

            if (film==null)
            {
                //парсер с сайта
            }
            return film;
        }

        public async Task DeleteFilm(int? id)
        {
            var film = await _dbContext.Films.FirstOrDefaultAsync(p => p.Id == id);
            if (film != null)
            {
                _dbContext.Films.Remove(film);
                _dbContext.SaveChanges();
            }
        }

        public async Task<Film> ConfirmDeleteFilm(int? id)
        {
            Film? film = await _dbContext.Films.FirstOrDefaultAsync(p => p.Id == id);
            return film ?? throw new Exception();
        }

        public async Task<List<Film>> GetAllFilm()
        {
            var allFilms = await _dbContext.Films.ToListAsync();
            return allFilms;
        }

        public async Task<Film> GetFilm(int? id)
        {
            var film = await _dbContext.Films.FirstOrDefaultAsync(p => p.Id == id);
            return film;
        }

        public async Task<Film> UpdateFilm(int? id)
        {
            Film? film = await _dbContext.Films.FirstOrDefaultAsync(p => p.Id == id);
            return film;
        }

        public async Task UpdateFilm(Film film)
        {
            _dbContext.Films.Update(film);
            _dbContext.SaveChanges();
        }

        public async Task<Film> GetFilmByName(string name)
        {
            Film? film = await _dbContext.Films.FirstOrDefaultAsync(p => p.Name == name);
            return film;
        }

        public async Task<Film> GetFilmByNameAndDate(string name, string date)
        {
            Film? film = await _dbContext.Films.FirstOrDefaultAsync(p => p.Name == name && p.ReleaseData==date);
            return film;
        }
    }
}
