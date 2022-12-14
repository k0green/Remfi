using NetflixMVC.Entities;

namespace NetflixMVC.Interfaces
{
    public interface IUserCrudlService : ICreateService, IDeleteService, IUpdateService, IGetAllService
    {
        public Task<List<User>> GetAllUser();
        public Task<User> GetUser(int id);
        public Task<User> GetUserByLogin(string login);
        //public Task CreateUser(string name, string login, string password, int? roleId);
        public Task CreateUser(RegisterModel model);
    }
}
