using NetflixMVC.Entities;

namespace NetflixMVC.Interfaces
{
    public interface IUserFilmDetailsCrudlService
    {
        public Task AddMark(int? filmId, int? userId, float mark);
        public Task AddComment(int? filmId, int? userId, string comments);
        public Task<float> GetMark(int? filmId, int? userId);
        public Task<string> GetComment(int? filmId, int? userId);
        public Task EditMark(int? filmId, int? userId);
        public Task EditComment(int? filmId, int? userId);
        public Task DeleteMark(int? filmId, int? userId);
        public Task DeleteComment(int? filmId, int? userId);
    }
}
