using Drones.Application.Interfaces;
using Drones.Application.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Drones.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblies(
                AppDomain.CurrentDomain.GetAssemblies().Where(w => !w.IsDynamic));

            services.AddAutoMapper(cfg => cfg.AddMaps(Assembly.GetExecutingAssembly()));

            services.AddScoped<IMedicationApplication, MedicationApplication>();
            services.AddScoped<IDroneApplication, DroneApplication>();
            services.AddScoped<IDispatchApplication, DispatchApplication>();

            return services;
        }
    }
}
