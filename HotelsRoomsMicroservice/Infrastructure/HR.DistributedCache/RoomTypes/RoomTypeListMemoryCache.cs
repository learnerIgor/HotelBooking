using HR.Application.Abstractions.Caches.RoomTypes;
using HR.Application.Dtos;
using HR.Application.Handlers.RoomTypes;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.RoomTypes
{
    public class RoomTypeListMemoryCache : BaseCache<BaseListDto<GetRoomTypeDto>>, IRoomTypeListMemoryCache
    {
        public RoomTypeListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
