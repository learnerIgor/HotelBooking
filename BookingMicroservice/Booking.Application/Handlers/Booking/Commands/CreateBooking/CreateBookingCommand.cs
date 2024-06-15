using MediatR;

namespace Booking.Application.Handlers.Booking.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<GetBookingDto>
    {
        public string RoomId { get; set; } = default!;
        public string StartDate { get; init; } = default!;
        public string EndDate { get; init; } = default!;
    }
}
