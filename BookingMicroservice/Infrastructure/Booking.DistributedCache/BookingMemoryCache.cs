using Booking.Application.Abstractions.Caches;
using Booking.Application.Handlers.Booking;
using Microsoft.Extensions.Caching.Distributed;

namespace Booking.DistributedCache
{
    public class BookingMemoryCache : BaseCache<GetBookingDto>, IBookingMemoryCache
    {
        public BookingMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}