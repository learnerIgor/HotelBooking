using Booking.Application.Abstractions.Caches;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booking.DistributedCache
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDistributedCacheServices(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            })
                .AddTransient<IBookingListMemoryCache, BookingListMemoryCache>()
                .AddTransient<IBookingMemoryCache, BookingMemoryCache>()
                .AddTransient<IBookingCountMemoryCache, BookingCountMemoryCache>()
                .AddTransient<RedisService>();
        }
    }
}
