using Booking.Application.Abstractions.Caches;
using Booking.Application.Dtos;
using Booking.Application.Handlers.Booking;
using Microsoft.Extensions.Caching.Distributed;

namespace Booking.DistributedCache
{
    public class BookingListMemoryCache : BaseCache<BaseListDto<GetBookingDto>>, IBookingListMemoryCache
    {
        public BookingListMemoryCache(IDistributedCache distributedCache, RedisService redisServer) : base(distributedCache, redisServer)
        {
        }
    }
}
