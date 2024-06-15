using HR.Application.Dtos;
using HR.Application.Handlers.Hotels;

namespace HR.Application.Abstractions.Caches.Hotels
{
    public interface IHotelListMemoryCache : IBaseCache<BaseListDto<GetHotelDto>>
    {
    }
}
