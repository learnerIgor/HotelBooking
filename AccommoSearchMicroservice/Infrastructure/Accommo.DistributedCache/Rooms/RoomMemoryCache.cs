using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Dtos.Rooms;
using Microsoft.Extensions.Caching.Distributed;

namespace Accommo.DistributedCache.Rooms
{
    public class RoomMemoryCache : BaseCache<GetRoomDto>, IRoomMemoryCache
    {
        public RoomMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
