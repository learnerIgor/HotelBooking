using Booking.Application.Handlers.Booking;

namespace Booking.Application.Abstractions.Caches
{
    public interface IBookingMemoryCache : IBaseCache<GetBookingDto>
    {
    }
}
