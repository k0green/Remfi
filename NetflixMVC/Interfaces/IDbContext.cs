using Microsoft.EntityFrameworkCore;
using NetflixMVC.Entities;

namespace NetflixMVC.Interfaces
{
    public interface IDbContext
    {
        DbSet<Film> Films { get; set; }
        DbSet<Series> Series { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<Userfilm> Userfilms { get; set; }
        DbSet<Usersfilmsdetail> Usersfilmsdetails { get; set; }
        DbSet<Usersseriesdetail> Usersseriesdetails { get; set; }


        int SaveChanges();
    }
}
