using Booking.Application.Abstractions.Caches;

namespace Booking.Application.Caches;

public class CleanBookingCacheService : ICleanBookingCacheService
{
    private readonly IBookingMemoryCache _bookingMemoryCache;
    private readonly IBookingListMemoryCache _bookingListMemoryCache;

    public CleanBookingCacheService(
        IBookingMemoryCache bookingMemoryCache,
        IBookingListMemoryCache bookingListMemoryCache)
    {
        _bookingListMemoryCache = bookingListMemoryCache;
        _bookingMemoryCache = bookingMemoryCache;
    }

    public void ClearAllCaches()
    {
        _bookingMemoryCache.Clear();
        _bookingListMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _bookingListMemoryCache.Clear();
    }
}