using NetflixMVC.Interfaces;

namespace NetflixMVC.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IDbContext _dbContext;

        public RegistrationService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}