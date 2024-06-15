using HR.Application.Abstractions.Caches.RoomTypes;
using HR.Application.Handlers.RoomTypes;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.RoomTypes
{
    public class RoomTypeMemoryCache : BaseCache<GetRoomTypeDto>, IRoomTypeMemoryCache
    {
        public RoomTypeMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
