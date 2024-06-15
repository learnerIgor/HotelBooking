using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.DistributedCache.Hotels;
using Accommo.DistributedCache.Rooms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Accommo.DistributedCache
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDistributedCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            })
                .AddTransient<IHotelListMemoryCache,HotelListMemoryCache>()
                .AddTransient<IHotelMemoryCache,HotelMemoryCache>()
                .AddTransient<IRoomListMemoryCache, RoomListMemoryCache>()
                .AddTransient<IRoomMemoryCache, RoomMemoryCache>()
                .AddTransient<IRoomBookMemoryCache, RoomBookMemoryCache>()
                .AddTransient<RedisService>();
        }
    }
}
