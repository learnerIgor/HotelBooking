using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Dtos;
using HR.Application.Handlers.Location.Cities;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Cities
{
    public class CityListMemoryCache : BaseCache<BaseListDto<GetCityDto>>, ICityListMemoryCache
    {
        public CityListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
