namespace HR.Application.Caches;

public interface ICleanHotelRoomCacheService
{
    void ClearAllCaches();
    void ClearListCaches();
}