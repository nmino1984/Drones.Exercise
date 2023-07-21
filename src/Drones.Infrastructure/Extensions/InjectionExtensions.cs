using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Infrastructure.Persistences.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drones.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DronesContext).Assembly.FullName;

            services.AddDbContext<DronesContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("POSConnectionString"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            return services;
        }
    }
}
