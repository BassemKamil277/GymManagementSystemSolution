using AutoMapper;
using GymManagementBLL;
using GymManagementBLL.Services.AttachmentService;
using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using GymManagementDAL.Data.Contexts;
using GymManagementDAL.Data.DataSeed;
using GymManagementDAL.Entities;
using GymManagementDAL.Repositories.Classes;
using GymManagementDAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
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
            builder.Services.AddScoped<IUintOFWork, UintOfWork>();
            builder.Services.AddScoped<ISessionRepository , SessionRepository>();
            builder.Services.AddAutoMapper(x => x.AddProfile(new MappingProfile()));
            builder.Services.AddScoped<IAnalyticsService, AnalyticsService>();
            builder.Services.AddScoped<IMemberService, MemberService>();
            builder.Services.AddScoped<ITrainerService, TrainerService>();
            builder.Services.AddScoped<IPlanService, PlanService>();
            builder.Services.AddScoped<ISessionService, SessionService>();
            builder.Services.AddScoped<IAttachmentService, AttachmentService>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(Config =>
            {
                //Config.Password.RequiredLength = 6;
                //Config.Password.RequireLowercase = true;
                //Config.Password.RequireUppercase = true;  kda kda dol mawgoden msh lazm aktbhom
                Config.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<GymDbContext>();
            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.LogoutPath = "/Account/Login";
                option.AccessDeniedPath = "/Account/AccessDenied";
            });
            builder.Services.AddScoped<IAccountService, AccountService>();






            var app = builder.Build();

            #region Seed Data - Migrate data base 
            //el tari2a dy 34an a3rf akhod object mn dbContext zy m b3ml bs hna msh haynf3 a3ml constractor
            using var Scoped = app.Services.CreateScope(); // kda ana mskt el scope ely feh kol el objects ely m3mol leha allow ll debendancy injections ely el live time bta3ha "scope" zy el "dbcontext" aw "unitOfWork"
            var dbContext = Scoped.ServiceProvider.GetRequiredService<GymDbContext>(); // kda ana msk el object ely 3aizo 
            var roleManager = Scoped.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = Scoped.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var PendingMigrations = dbContext.Database.GetPendingMigrations(); // da byshof ay migrations msh m3mol leha update fl data base
            if (PendingMigrations?.Any() ?? false)
                dbContext.Database.Migrate();
             

            GymDbContextDataSeeding.SeedData(dbContext);
            IdentityDbContextSeeding.SeedData(roleManager, userManager);

            #endregion




            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
