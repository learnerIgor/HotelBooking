using MediatR;

namespace Accommo.Application.Handlers.External.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
