using Booking.Application.Dtos;
using MediatR;

namespace Booking.Application.Handlers.Booking.Queries.GetUserBookings
{
    public class GetUserBookingsQuery : IRequest<BaseListDto<GetBookingDto>>
    {
        public string ApplicationUserId { get; init; } = default!;
    }
}
