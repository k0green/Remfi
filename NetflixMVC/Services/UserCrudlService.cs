using Microsoft.EntityFrameworkCore;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;

namespace NetflixMVC.Services
{
    public class UserCrudlService : IUserCrudlService
    {

        private readonly IDbContext _dbContext;

        public UserCrudlService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<User>> GetAllUser()
        {
            var allUser = await _dbContext.Users.ToListAsync();
            return allUser;
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(d => d.Id == id);
            return user ?? throw new Exception();
        }

        public async Task<User> GetUserByLogin(string login)
        {

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
            return user;
        }

        public async Task CreateUser(string name, string login, string password)
        {
            await _dbContext.Users
                .AddAsync(new User 
                { 
                    Name = name, 
                    Login = login, 
                    Password = IRegistrationService.HashPassword(password)
                });
            _dbContext.SaveChanges();
        }
    }
}
