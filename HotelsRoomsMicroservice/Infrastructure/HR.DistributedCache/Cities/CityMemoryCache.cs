using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Handlers.Location.Cities;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Cities
{
    public class CityMemoryCache : BaseCache<GetCityDto>, ICityMemoryCache
    {
        public CityMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}