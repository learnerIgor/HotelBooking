using MediatR;

namespace Booking.Application.Handlers.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
