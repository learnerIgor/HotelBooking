using HR.Application.Dtos;
using HR.Application.Handlers.Location.Countries;

namespace HR.Application.Abstractions.Caches.Locations.Countries
{
    public interface ICountryListMemoryCache : IBaseCache<BaseListDto<GetCountryDto>>
    {
    }
}
