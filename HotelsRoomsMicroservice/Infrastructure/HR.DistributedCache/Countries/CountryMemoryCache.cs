using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Application.Handlers.Location.Countries;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Countries
{
    public class CountryMemoryCache : BaseCache<GetCountryDto>, ICountryMemoryCache
    {
        public CountryMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}