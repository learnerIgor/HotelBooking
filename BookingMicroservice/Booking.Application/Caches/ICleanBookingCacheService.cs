namespace Booking.Application.Caches;

public interface ICleanBookingCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}