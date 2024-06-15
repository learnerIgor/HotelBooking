using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Handlers.Hotels;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Hotels
{
    public class HotelMemoryCache : BaseCache<GetHotelDto>, IHotelMemoryCache
    {
        public HotelMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}