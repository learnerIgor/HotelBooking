using Booking.Application.Abstractions.Caches;
using Booking.Application.Handlers.Booking;
using Microsoft.Extensions.Caching.Distributed;

namespace Booking.DistributedCache
{
    public class BookingCountMemoryCache : BaseCache<int>, IBookingCountMemoryCache
    {
        public BookingCountMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
