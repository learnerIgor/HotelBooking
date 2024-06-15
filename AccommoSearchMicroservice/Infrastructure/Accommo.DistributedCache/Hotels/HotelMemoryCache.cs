using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Dtos.Hotels;
using Microsoft.Extensions.Caching.Distributed;

namespace Accommo.DistributedCache.Hotels
{
    public class HotelMemoryCache : BaseCache<GetHotelDto>, IHotelMemoryCache
    {
        public HotelMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}