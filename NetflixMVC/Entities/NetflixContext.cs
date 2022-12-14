using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using NetflixMVC.Interfaces;
using NetflixMVC.Entities;


namespace NetflixMVC
{
    public partial class NetflixContext : DbContext, IDbContext
    {
        public NetflixContext()
        {
        }

        public NetflixContext(DbContextOptions<NetflixContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Series> Series { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Userfilm> Userfilms { get; set; } = null!;
        public virtual DbSet<Usersfilmsdetail> Usersfilmsdetails { get; set; } = null!;
        public virtual DbSet<Usersseriesdetail> Usersseriesdetails { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=127.0.0.1;database=Netflix;uid=root;pwd=12345678", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("film");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Country)
                    .HasMaxLength(30)
                    .HasColumnName("country");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.DurationTime)
                    .HasMaxLength(30)
                    .HasColumnName("duration_time");

                entity.Property(e => e.Genre)
                    .HasMaxLength(50)
                    .HasColumnName("genre");

                entity.Property(e => e.Link)
                    .HasColumnType("text")
                    .HasColumnName("link");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.NumberOfView).HasColumnName("numberOfView");

                entity.Property(e => e.Producer)
                    .HasMaxLength(40)
                    .HasColumnName("producer");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.ReleaseData)
                    .HasMaxLength(30)
                    .HasColumnName("release_data");

                entity.Property(e => e.CheckSeries).HasColumnName("series");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Series>(entity =>
            {
                entity.ToTable("series");

                entity.HasIndex(e => e.FilmId, "filmId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FilmId).HasColumnName("filmId");

                entity.Property(e => e.NumberOfSeries).HasColumnName("numberOfSeries");

                entity.Property(e => e.NumdersOfViews).HasColumnName("numdersOfViews");

                entity.Property(e => e.Season).HasColumnName("season");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.SeriesNavigation)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("series_ibfk_1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Login, "login")
                    .IsUnique();

                entity.HasIndex(e => e.RoleId, "role_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .HasMaxLength(75)
                    .HasColumnName("login");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(75)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId).HasColumnName("roleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("role_fk");
            });

            modelBuilder.Entity<Userfilm>(entity =>
            {
                entity.ToTable("userfilm");

                entity.HasIndex(e => e.FilmId, "filmId");

                entity.HasIndex(e => e.UserId, "userId");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Favorites).HasColumnName("favorites");

                entity.Property(e => e.FilmId).HasColumnName("filmId");

                entity.Property(e => e.NumberOfView).HasColumnName("numberOfView");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Userfilms)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("userfilm_ibfk_2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Userfilms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("userfilm_ibfk_1");
            });

            modelBuilder.Entity<Usersfilmsdetail>(entity =>
            {
                entity.ToTable("usersfilmsdetails");

                entity.HasIndex(e => e.FilmId, "userSeries_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.Favorites).HasColumnName("favorites");

                entity.Property(e => e.FilmId).HasColumnName("filmId");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Film)
                    .WithMany(p => p.Usersfilmsdetails)
                    .HasForeignKey(d => d.FilmId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("filmDetails_fk");
            });

            modelBuilder.Entity<Usersseriesdetail>(entity =>
            {
                entity.ToTable("usersseriesdetails");

                entity.HasIndex(e => e.SeriesId, "seriesDetails_fk");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Comment)
                    .HasColumnType("text")
                    .HasColumnName("comment");

                entity.Property(e => e.Mark).HasColumnName("mark");

                entity.Property(e => e.NumberOfView).HasColumnName("numberOfView");

                entity.Property(e => e.SeriesId).HasColumnName("seriesId");

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.Series)
                    .WithMany(p => p.Usersseriesdetails)
                    .HasForeignKey(d => d.SeriesId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("seriesDetails_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
