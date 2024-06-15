using Accommo.Application.Dtos;
using Accommo.Application.Dtos.Rooms;

namespace Accommo.Application.Abstractions.Caches.Rooms
{
    public interface IRoomListMemoryCache : IBaseCache<BaseListDto<GetRoomDto>>
    {
    }
}
