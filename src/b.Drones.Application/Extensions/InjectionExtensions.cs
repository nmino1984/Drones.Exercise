using Drones.Infrastructure.Persistences.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Drones.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
            {
                var assembly = typeof(DronesContext).Assembly.FullName;

                services.AddDbContext<DronesContext>(
                    options => options.UseSqlServer(
                        configuration.GetConnectionString("DBConnectionString"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

                return services;
            }
    }

    
}
