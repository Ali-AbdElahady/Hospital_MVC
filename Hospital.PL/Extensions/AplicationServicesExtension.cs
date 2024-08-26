using Hospital.BLL.Interfaces;
using Hospital.BLL.Repositories;

namespace Hospital.PL.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            return Services;
        }
    }
}
