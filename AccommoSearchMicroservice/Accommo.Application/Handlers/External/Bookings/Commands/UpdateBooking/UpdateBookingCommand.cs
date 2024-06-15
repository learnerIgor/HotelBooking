using MediatR;

namespace Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommand : IRequest<GetBookingDto>
    {
        public string ReservationId { get; set; } = default!;
        public string StartDate { get; init; } = default!;
        public string EndDate { get; init; } = default!;
    }
}
