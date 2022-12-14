using NetflixMVC.Entities;
using NetflixMVC.Models;

namespace NetflixMVC.Interfaces
{
    public interface IFilmCrudlService : ICreateService, IDeleteService, IUpdateService, IGetAllService, IGetOneService
    {
        public Task<List<Film>> GetAllFilm();
        public Task<Film> GetFilm(int? id);
        public Task<Film> GetFilmByName(string name);
        public Task<Film> GetFilmByNameAndDate(string name, string date);
        public Task<List<FilmUserDisplayModel>> GetAllFilmForOneUser(int userId);
        public Task<Film> FindFilm(string name);
        public Task CreateFilm(Film film);
        public Task<Film> UpdateFilm(int? id);
        public Task UpdateFilm(Film film);
        public Task DeleteFilm(int? id);
        public Task<Film> ConfirmDeleteFilm(int? id);
    }
}
