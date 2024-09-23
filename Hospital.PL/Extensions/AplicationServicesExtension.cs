using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;
using Hospital.PL.Helpers;
using Hospital.PL.Services;
using Hospital.PL.Utilities;
using Hospital.PL.Utlities;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Hospital.PL.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IDbInitializer), typeof(DbInitializer));
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            Services.AddScoped(typeof(IEmailSender), typeof(EmailSender));
            Services.AddScoped(typeof(IUserServices), typeof(UserServices));
            Services.AddAutoMapper(typeof(MappingProfiles));
            //Services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
                });
            Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

            return Services;
        }
    }
}
