using HR.Application.Abstractions.Caches.Rooms;
using HR.Application.Dtos;
using HR.Application.Handlers.Rooms;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Rooms
{
    public class RoomListMemoryCache : BaseCache<BaseListDto<GetRoomDto>>, IRoomListMemoryCache
    {
        public RoomListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
