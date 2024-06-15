using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Dtos;
using Accommo.Application.Dtos.Hotels;
using Microsoft.Extensions.Caching.Distributed;

namespace Accommo.DistributedCache.Hotels
{
    public class HotelListMemoryCache : BaseCache<BaseListDto<GetHotelDto>>, IHotelListMemoryCache
    {
        public HotelListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
