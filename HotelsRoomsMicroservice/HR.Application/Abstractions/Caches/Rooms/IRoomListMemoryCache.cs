using HR.Application.Dtos;
using HR.Application.Handlers.Rooms;

namespace HR.Application.Abstractions.Caches.Rooms
{
    public interface IRoomListMemoryCache : IBaseCache<BaseListDto<GetRoomDto>>
    {
    }
}
