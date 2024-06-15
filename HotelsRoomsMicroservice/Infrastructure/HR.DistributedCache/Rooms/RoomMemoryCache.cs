using HR.Application.Abstractions.Caches.Rooms;
using HR.Application.Handlers.Rooms;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Rooms
{
    public class RoomMemoryCache : BaseCache<GetRoomDto>, IRoomMemoryCache
    {
        public RoomMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
