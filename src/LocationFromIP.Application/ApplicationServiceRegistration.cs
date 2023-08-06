using LocationFromIP.Application.Interfaces.Services;
using LocationFromIP.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LocationFromIP.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<ILocationService, LocationService>();

            return services;
        }
    }
}