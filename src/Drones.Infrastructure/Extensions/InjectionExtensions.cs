using Drones.Infrastructure.Persistences.Contexts;
using Drones.Infrastructure.Persistences.Interfaces;
using Drones.Infrastructure.Persistences.Repositories;
using Drones.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Drones.Domain.Entities;

namespace Drones.Infrastructure.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = typeof(DronesContext).Assembly.FullName;

            services.AddDbContext<DronesContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DBConnectionString"), b => b.MigrationsAssembly(assembly)), ServiceLifetime.Transient);

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBatteryLogRepository, BatteryLogRepository>();

            services.AddHostedService<BatteryMonitorService>();

            return services;
        }
    }
}
