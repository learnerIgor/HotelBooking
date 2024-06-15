using HR.Application.Handlers.Location.Countries;

namespace HR.Application.Abstractions.Caches.Locations.Countries
{
    public interface ICountryMemoryCache : IBaseCache<GetCountryDto>
    {
    }
}
