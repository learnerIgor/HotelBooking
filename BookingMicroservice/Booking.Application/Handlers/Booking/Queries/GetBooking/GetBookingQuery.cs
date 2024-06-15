using MediatR;

namespace Booking.Application.Handlers.Booking.Queries.GetBooking
{
    public class GetBookingQuery : IRequest<GetBookingDto>
    {
        public string ReservationId { get; init; } = default!;
    }
}
