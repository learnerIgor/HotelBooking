using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Dtos;
using HR.Application.Handlers.Hotels;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Hotels
{
    public class HotelListMemoryCache : BaseCache<BaseListDto<GetHotelDto>>, IHotelListMemoryCache
    {
        public HotelListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
