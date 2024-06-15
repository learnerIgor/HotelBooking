using HR.Application.Dtos;
using HR.Application.Handlers.RoomTypes;

namespace HR.Application.Abstractions.Caches.RoomTypes
{
    public interface IRoomTypeListMemoryCache : IBaseCache<BaseListDto<GetRoomTypeDto>>
    {
    }
}
