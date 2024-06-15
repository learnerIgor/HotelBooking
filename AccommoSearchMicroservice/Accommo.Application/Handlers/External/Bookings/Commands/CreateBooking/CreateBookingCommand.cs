using MediatR;

namespace Accommo.Application.Handlers.External.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommand : IRequest<GetBookingDto>
    {
        public string ReservationId { get; init; } = default!;
        public string CheckInDate { get; init; } = default!;
        public string CheckOutDate { get; init; } = default!;
        public bool IsActive { get; init; }
        public string RoomId { get; init; } = default!;
    }
}
