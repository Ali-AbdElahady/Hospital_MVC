using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;
using Hospital.PL.Utilities;

namespace Hospital.PL.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IDbInitializer), typeof(DbInitializer));
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            return Services;
        }
    }
}
