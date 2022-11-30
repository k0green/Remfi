using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using System.Xml.Linq;

namespace NetflixMVC.Services
{
    public class UserFilmDetailsCrudlService : IUserFilmDetailsCrudlService
    {
        private readonly IDbContext _dbContext;

        public UserFilmDetailsCrudlService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddMark(int? filmId, int? userId, float mark)
        {
            Usersfilmsdetail usersFilmsDetail = new();
            var usersFilmsDetailChecked = _dbContext.Usersfilmsdetails.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);
            usersFilmsDetail.Mark = mark;
            usersFilmsDetail.UserId = userId;
            usersFilmsDetail.FilmId = filmId;
            if (usersFilmsDetailChecked == null)
            {
                _dbContext.Usersfilmsdetails.Add(usersFilmsDetail);
            }
            else
            {
                usersFilmsDetailChecked.Mark = mark;
            }
            _dbContext.SaveChanges();
        }
        public async Task AddComment(int? filmId, int? userId, string comments)
        {
            Usersfilmsdetail usersFilmsDetail = new Usersfilmsdetail();
            var usersFilmsDetailChecked = _dbContext.Usersfilmsdetails.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);
                usersFilmsDetail.Comment = comments;
                usersFilmsDetail.UserId = userId;
                usersFilmsDetail.FilmId = filmId;
            if (usersFilmsDetailChecked == null)
            {
                _dbContext.Usersfilmsdetails.Add(usersFilmsDetail);
            }
            else
            {
                usersFilmsDetailChecked.Comment = comments;
            }
            _dbContext.SaveChanges();
        }

        public async Task<float> GetMark(int? filmId, int? userId)
        {
            var usersfilmsdetail = _dbContext.Usersfilmsdetails.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);
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

        public async Task<string> GetComment(int? filmId, int? userId)
        {
            var comment = _dbContext.Usersfilmsdetails.FirstOrDefault(p => p.UserId == userId && p.FilmId == filmId);
            if (comment == null)
            {
                return "empty";
            }
            else
            {
                return comment.Comment;
            }
        }

        public Task EditMark(int? filmId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task EditComment(int? filmId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMark(int? filmId, int? userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteComment(int? filmId, int? userId)
        {
            throw new NotImplementedException();
        }
    }
}
