using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Application.Abstractions.Caches.Rooms;
using HR.Application.Abstractions.Caches.RoomTypes;
using HR.DistributedCache.Cities;
using HR.DistributedCache.Countries;
using HR.DistributedCache.Hotels;
using HR.DistributedCache.Rooms;
using HR.DistributedCache.RoomTypes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.DistributedCache
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDistributedCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            })
                .AddTransient<IHotelListMemoryCache, HotelListMemoryCache>()
                .AddTransient<IHotelMemoryCache, HotelMemoryCache>()
                .AddTransient<IRoomListMemoryCache, RoomListMemoryCache>()
                .AddTransient<IRoomMemoryCache, RoomMemoryCache>()
                .AddTransient<IRoomTypeListMemoryCache, RoomTypeListMemoryCache>()
                .AddTransient<IRoomTypeMemoryCache, RoomTypeMemoryCache>()
                .AddTransient<ICityMemoryCache, CityMemoryCache>()
                .AddTransient<ICityListMemoryCache, CityListMemoryCache>()
                .AddTransient<ICountryMemoryCache, CountryMemoryCache>()
                .AddTransient<ICountryListMemoryCache, CountryListMemoryCache>()
                .AddTransient<RedisService>();
        }
    }
}
