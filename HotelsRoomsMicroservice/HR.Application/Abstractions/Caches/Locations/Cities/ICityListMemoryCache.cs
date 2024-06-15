using HR.Application.Dtos;
using HR.Application.Handlers.Location.Cities;

namespace HR.Application.Abstractions.Caches.Locations.Cities
{
    public interface ICityListMemoryCache : IBaseCache<BaseListDto<GetCityDto>>
    {
    }
}
