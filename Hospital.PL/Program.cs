using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;
using Hospital.DAL.Context;
using Hospital.DAL.Entites;
using Hospital.PL.Extensions;
using Hospital.PL.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Hospital.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Add Extenssions
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddApplicationServices();


            var app = builder.Build();

            #region seeding data and update migration
            // Ask CLR For Creating Object From DbContext Explicitly
            using var Scope = app.Services.CreateScope();
            // Group Of Services LifeTime Scoped
            var Services = Scope.ServiceProvider;
            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<HospitalDbContext>();
                await dbContext.Database.MigrateAsync();

                await HospitalContextSeed.SeedAsync(dbContext);

                var dbInitial = Services.GetRequiredService<IDbInitializer>();
                await dbInitial.Initialize();

            }
            catch (Exception ex)
            {
                var Logger = LoggerFactory.CreateLogger<Program>();
                Logger.LogError(ex, "An Error Occured During Appling The Migration");
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}