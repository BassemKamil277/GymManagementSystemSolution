using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagementPl
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<GymDbContext>(options =>
            {
                //options.UseSqlServer("Server = .;Database = GymManagementGroup01;Trusted_Connection = true;TrustServerCertificate = true")
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            //builder.Services.AddScoped<GenaricRepository<Member>, GenaricRepository<Member>>();
            /*builder.Services.AddScoped(typeof(GenaricRepository<>) , typeof(GenaricRepository<>));*/ // b2olo en wa2t m t7tag GenaricRepository of ay haga da hay3mlo
            //builder.Services.AddScoped(typeof(IGenaricRepository<>), typeof(GenaricRepository<>)); // b2olo en wa2t m t7tag GenaricRepository of ay haga da hay3mlo...w 3mltha 3la el interface 34an y2bl el inter face nafsha aw ay haga btwrs mnha 
            //builder.Services.AddScoped<IPlanRepository , PlanRepository>();
            builder.Services.AddScoped<IUintOFWork ,UintOFWork>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
