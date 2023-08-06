using LocationFromIP.Application.Interfaces.Geoapify;
using LocationFromIP.Infrastructure.Geoapify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocationFromIP.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var geoapifySection = configuration.GetSection("Services:Geoapify");

            services.AddHttpClient<IGeoapifyClient, GeoapifyClient>(client =>
            {
                client.BaseAddress = new Uri(geoapifySection["BaseUrl"]);
                client.Timeout = TimeSpan.Parse(geoapifySection["Timeout"]);
                client.DefaultRequestHeaders.Clear();
            });

            return services;
        }
    }
}