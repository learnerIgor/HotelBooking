using Accommo.Application.Dtos;
using Accommo.Application.Dtos.Hotels;

namespace Accommo.Application.Abstractions.Caches.Hotels
{
    public interface IHotelListMemoryCache : IBaseCache<BaseListDto<GetHotelDto>>
    {
    }
}
