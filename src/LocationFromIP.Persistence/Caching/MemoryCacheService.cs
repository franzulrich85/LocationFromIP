using LocationFromIP.Application.Interfaces.Persistence;
using LocationFromIP.Application.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace LocationFromIP.Persistence.Caching
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IOptionsMonitor<CacheOption> _cacheConfig;
        private MemoryCacheEntryOptions _cacheOptions;

        public MemoryCacheService(IMemoryCache memoryCache, IOptionsMonitor<CacheOption> cacheConfig)
        {
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig;
            if (_cacheConfig != null)
            {
                _cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddMinutes(_cacheConfig.CurrentValue.AbsoluteExpirationInMinutes),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.CurrentValue.SlidingExpirationInMinutes)
                };
            }
        }
        public bool TryGet<T>(string cacheKey, out T value)
        {
            _memoryCache.TryGetValue(cacheKey, out value);
            if (value == null) return false;
            else return true;
        }

        public T Set<T>(string cacheKey, T value)
        {
            return _memoryCache.Set(cacheKey, value, _cacheOptions);
        }

        public void Remove(string cacheKey)
        {
            _memoryCache.Remove(cacheKey);
        }
    }
}