using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Application.Dtos;
using HR.Application.Handlers.Location.Countries;
using Microsoft.Extensions.Caching.Distributed;

namespace HR.DistributedCache.Countries
{
    public class CountryListMemoryCache : BaseCache<BaseListDto<GetCountryDto>>, ICountryListMemoryCache
    {
        public CountryListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
