using Booking.Application.Dtos;
using Booking.Application.Handlers.Booking;

namespace Booking.Application.Abstractions.Caches
{
    public interface IBookingListMemoryCache : IBaseCache<BaseListDto<GetBookingDto>>
    {
    }
}
