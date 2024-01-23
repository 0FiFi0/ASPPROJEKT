using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        private string DbPath { get; set; }

        public AppDbContext()
        {
            var str = Environment.CurrentDirectory;
            str = str.Substring(0, str.LastIndexOf('\\') + 1);
            str = str + "Data";
            var folder = Path.Combine(str, "BazaDanych");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            DbPath = Path.Combine(folder, "Photos.db");
        }

        public DbSet<PhotoEntity> Photos { get; set; }
        public DbSet<AuthorEntity> Authors { get; set; }
        public DbSet<AddressEntity> Address { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data source={DbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@wsei.edu.pl",
                NormalizedEmail = "USER@WSEI.EDU.PL",
                EmailConfirmed = true,
            };

            var admin = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@wsei.edu.pl",
                NormalizedEmail = "ADMIN@WSEI.EDU.PL",
                EmailConfirmed = true,
            };

            user.PasswordHash = ph.HashPassword(user, "user");
            admin.PasswordHash = ph.HashPassword(admin, "admin123");

            modelBuilder.Entity<IdentityUser>()
                .HasData(
                        user, admin
                    );
            var userRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "user",
                NormalizedName = "USER"
            };
            var adminRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            adminRole.ConcurrencyStamp = adminRole.Id;
            userRole.ConcurrencyStamp = userRole.Id;

            modelBuilder.Entity<IdentityRole>()
                .HasData(
                    adminRole, userRole
                );

            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = userRole.Id,
                    UserId = user.Id,
                }, new IdentityUserRole<string>()
                {
                    RoleId = adminRole.Id,
                    UserId = admin.Id,
                }
                );

            modelBuilder.Entity<PhotoEntity>()
                .HasOne(c => c.Author)
                .WithMany(o => o.Photos)
                .HasForeignKey(c => c.AuthorId);

            modelBuilder.Entity<AuthorEntity>()
                .HasOne(c => c.Address)
                .WithMany(o => o.Authors)
                .HasForeignKey(c => c.AddressId);


            modelBuilder.Entity<AddressEntity>()
                .HasKey(o => o.Id);


            modelBuilder.Entity<AddressEntity>()
                .HasData(
                    new AddressEntity()
                    {
                        Id = 1,
                        City = "Myślenice",
                        Street = "Średniawskiego 23",
                        PostalCode = "32-726"
                    },
                    new AddressEntity()
                    {
                        Id = 2,
                        City = "Rudnik",
                        Street = "Słoneczna 10",
                        PostalCode = "32-440"
                    }
                );

            modelBuilder.Entity<AuthorEntity>()
                .HasData(
                    new AuthorEntity()
                    {
                        Id = 1,
                        Name = "Maciek",
                        Description = "Maciej Nowak lat 15",
                        AddressId = 1
                    },
                    new AuthorEntity()
                    {
                        Id = 2,
                        Name = "Waldemar",
                        Description = "Waldemar Wolski lat 50",
                        AddressId = 1
                    },
                    new AuthorEntity()
                    {
                        Id = 3,
                        Name = "Kacper",
                        Description = "Kacper Kowalskie lat 22",
                        AddressId = 2
                    }
                );

            modelBuilder.Entity<PhotoEntity>().HasData(
                new PhotoEntity()
                {
                    PhotoId = 1,
                    Camera = "Sony",
                    Description = "Drzewo na tle zachodzącego słońca",
                    Resolution = "500x500",
                    CreatedDate = new DateTime(2023, 11, 30),
                    Format = PhotoFormat.JPEG,
                    AuthorId = 1
                },
                new PhotoEntity()
                {
                    PhotoId = 2,
                    Camera = "Nikon",
                    Description = "Latarnia na skale",
                    Resolution = "100x1000",
                    CreatedDate = new DateTime(2022, 11, 12),
                    Format = PhotoFormat.PNG,
                    AuthorId = 2
                },
                new PhotoEntity()
                {
                    PhotoId = 3,
                    Camera = "Sony",
                    Description = "Ulice miasta nocą",
                    Resolution = "1500x1500",
                    CreatedDate = new DateTime(2024, 1, 1),
                    Format = PhotoFormat.GIF,
                    AuthorId = 3
                },
                new PhotoEntity()

                {
                    PhotoId = 4,
                    Camera = "Polaroid",
                    Description = "Pustynia",
                    Resolution = "1520x1520",
                    CreatedDate = new DateTime(2002, 11, 11),
                    Format = PhotoFormat.PNG,
                    AuthorId = 2
                }

            );
        }
    }
}
