using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;

namespace Accommo.Application.Caches;

public class CleanAccommoCacheService : ICleanAccommoCacheService
{
    private readonly IHotelMemoryCache _hotelMemoryCache;
    private readonly IHotelListMemoryCache _hotelListMemoryCache;
    private readonly IRoomMemoryCache _roomMemoryCache;
    private readonly IRoomListMemoryCache _roomListMemoryCache;
    private readonly IRoomBookMemoryCache _roomBookMemoryCache;

    public CleanAccommoCacheService(
        IHotelMemoryCache hotelMemoryCache,
        IHotelListMemoryCache hotelListMemoryCache,
        IRoomMemoryCache roomMemoryCache,
        IRoomListMemoryCache roomListMemoryCache,
        IRoomBookMemoryCache roomBookMemoryCache)
    {
        _hotelMemoryCache = hotelMemoryCache;
        _hotelListMemoryCache = hotelListMemoryCache;
        _roomMemoryCache = roomMemoryCache;
        _roomListMemoryCache = roomListMemoryCache;
        _roomBookMemoryCache = roomBookMemoryCache;
    }

    public void ClearAllCaches()
    {
        _hotelMemoryCache.Clear();
        _hotelListMemoryCache.Clear();
        _roomMemoryCache.Clear();
        _roomListMemoryCache.Clear();
        _roomBookMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _hotelListMemoryCache.Clear();
        _roomListMemoryCache.Clear();
    }
}