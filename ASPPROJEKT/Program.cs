using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Data;
using System.Configuration;
using ASPPROJEKT.Models;

namespace ASPPROJEKT
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddRazorPages();
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<Data.AppDbContext>();
            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<Data.AppDbContext>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddTransient<IPhotoService, PhotoService>();
            builder.Services.AddTransient<IAuthorService, AuthorService>();
            builder.Services.AddTransient<IAddressService, AddressService>();

            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMiddleware<LastVisitCookie>();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSession();

            app.MapRazorPages();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
                name: "photo",
                pattern: "Photo/{action=Index}/{id?}",
                defaults: new { controller = "Photo" });

            app.Run();
        }
    }
}
