using LocationFromIP.Application.Interfaces.Persistence;
using LocationFromIP.Application.Options;
using LocationFromIP.Persistence.Caching;
using LocationFromIP.Persistence.DatabaseContext;
using LocationFromIP.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LocationFromIP.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //cache
            var cacheSection = configuration.GetSection("CacheSettings");
            services.Configure<CacheOption>(cacheSection);
            services.AddTransient<ICacheService,MemoryCacheService>();
            services.AddMemoryCache();

            //db
            services.AddDbContext<IpAddressDatabaseContext>(opt => opt.UseInMemoryDatabase(databaseName: $"IpAddressDatabase_{Guid.NewGuid()}"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IIpLocationRepository, IpLocationRepository>();

            return services;
        }
    }
}