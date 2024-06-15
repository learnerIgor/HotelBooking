using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Dtos;
using Accommo.Application.Dtos.Rooms;
using Microsoft.Extensions.Caching.Distributed;

namespace Accommo.DistributedCache.Rooms
{
    public class RoomListMemoryCache : BaseCache<BaseListDto<GetRoomDto>>, IRoomListMemoryCache
    {
        public RoomListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
