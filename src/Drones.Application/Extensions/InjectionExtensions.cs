using Drones.Application.Interfaces;
using Drones.Application.Services;
using Drones.Infrastructure.Persistences.Contexts;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Drones.Application.Extensions
{
    public static class InjectionExtensions
    {
        [Obsolete]
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(w => !w.IsDynamic));
            });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<IMedicationApplication, MedicationApplication>();
            services.AddScoped<IDroneApplication, DroneApplication>();
            services.AddScoped<IDroneMedicationApplication, DroneMedicationApplication>();

            return services;
        }
    }
}
