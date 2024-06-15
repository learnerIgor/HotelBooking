using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Application.Abstractions.Caches.Rooms;
using HR.Application.Abstractions.Caches.RoomTypes;

namespace HR.Application.Caches;

public class CleanHotelRoomCacheService : ICleanHotelRoomCacheService
{
    private readonly IHotelMemoryCache _hotelMemoryCache;
    private readonly IHotelListMemoryCache _hotelListMemoryCache;
    private readonly IRoomMemoryCache _roomMemoryCache;
    private readonly IRoomListMemoryCache _roomListMemoryCache;
    private readonly IRoomTypeMemoryCache _roomTypeMemoryCache;
    private readonly IRoomTypeListMemoryCache _roomTypeListMemoryCache;
    private readonly ICityMemoryCache  _cityMemoryCache;
    private readonly ICityListMemoryCache _cityListMemoryCache;
    private readonly ICountryMemoryCache _countryMemoryCache;
    private readonly ICountryListMemoryCache _countryListMemoryCache;

    public CleanHotelRoomCacheService(
        IHotelMemoryCache hotelMemoryCache,
        IHotelListMemoryCache hotelListMemoryCache,
        IRoomMemoryCache roomMemoryCache,
        IRoomListMemoryCache roomListMemoryCache,
        IRoomTypeMemoryCache roomTypeMemoryCache,
        IRoomTypeListMemoryCache roomTypeListMemoryCache,
        ICityListMemoryCache cityListMemoryCache,
        ICityMemoryCache cityMemoryCache,
        ICountryMemoryCache countryMemoryCache,
        ICountryListMemoryCache countryListMemoryCache)
    {
        _hotelMemoryCache = hotelMemoryCache;
        _hotelListMemoryCache = hotelListMemoryCache;
        _roomMemoryCache = roomMemoryCache;
        _roomListMemoryCache = roomListMemoryCache;
        _roomTypeMemoryCache = roomTypeMemoryCache;
        _roomTypeListMemoryCache = roomTypeListMemoryCache;
        _cityListMemoryCache = cityListMemoryCache;
        _cityMemoryCache = cityMemoryCache;
        _countryMemoryCache = countryMemoryCache;
        _countryListMemoryCache = countryListMemoryCache;
    }

    public void ClearAllCaches()
    {
        _hotelMemoryCache.Clear();
        _hotelListMemoryCache.Clear();
        _roomMemoryCache.Clear();
        _roomListMemoryCache.Clear();
        _roomTypeMemoryCache.Clear();
        _roomTypeListMemoryCache.Clear();
        _cityMemoryCache.Clear();
        _cityListMemoryCache.Clear();
        _countryMemoryCache.Clear();
        _countryListMemoryCache.Clear();
    }

    public void ClearListCaches()
    {
        _hotelListMemoryCache.Clear();
        _roomListMemoryCache.Clear();
        _roomTypeListMemoryCache.Clear();
        _cityListMemoryCache.Clear();
        _countryListMemoryCache.Clear();
    }
}