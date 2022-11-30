using NetflixMVC.Entities;
using NetflixMVC.Models;

namespace NetflixMVC.Interfaces
{
    public interface IUserFilmCrudlService
    {
        public List<FilmUserDisplayModel> GetAllFilmForUser(int? userId);
        public Task<Userfilm> GetOneFilmForUser(int? userId, int? filmId);
        public Task CreateUserFilmConnection(int? userId, string filmName, string filmDate);
        public Task AddAmountOfView(int? filmId, int userId);
        public Task<int> GetAmountOfView(int? filmId, int? userId);
        public Task AddToFavorite(int? filmId, int? userId, bool favorite);
        public Task<List<Userfilm>> GetToFavorite(int? userId);
        public Task<bool> GetToFavoriteValue(int? userId, int filmId);
        public FilmUserDisplayModel GetFindFilm(string name);

    }
}
