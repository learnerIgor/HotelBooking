using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Handlers.External.Rooms;
using Microsoft.Extensions.Caching.Distributed;

namespace Accommo.DistributedCache.Rooms
{
    public class RoomBookMemoryCache : BaseCache<GetRoomBookDto>, IRoomBookMemoryCache
    {
        public RoomBookMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
