
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using NetflixMVC;
using NetflixMVC.Entities;
using NetflixMVC.Interfaces;
using NetflixMVC.Services;

var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<NetflixContext>(options => options.UseMySql("server=127.0.0.1;database=Netflix;uid=root;pwd=12345678", ServerVersion.Parse("8.0.30-mysql")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
            options.LoginPath = new PathString("/Account/Login");
            options.AccessDeniedPath = new PathString("/Account/Login");
        });
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDbContext, NetflixContext>();
builder.Services.AddTransient<IFilmCrudlService, FilmCrudlService>();
builder.Services.AddTransient<IUserFilmCrudlService, UserFIlmCrudlService>();
builder.Services.AddTransient<IUserCrudlService, UserCrudlService>();
builder.Services.AddTransient<ISeriesCrudlService, SeriesCrudlService>();
builder.Services.AddTransient<IRegistrationService, RegistrationService>();
builder.Services.AddTransient<IUserFilmDetailsCrudlService, UserFilmDetailsCrudlService>();
builder.Services.AddTransient<IUserSeriesDetailsCrudlService, UserSeriesDetailsCrudlService>();



var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

    IServiceCollection services = new ServiceCollection();
{
    services.AddDbContext<NetflixContext>(options => options.UseMySql("server=127.0.0.1;database=Netflix;uid=root;pwd=12345678", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));

    services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => 
        {
        options.LoginPath = new PathString("/Account/Login");
    });
    services.AddControllersWithViews();
}

app.UseDeveloperExceptionPage();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Film}/{action=FindFilm}");
});

app.Run();
