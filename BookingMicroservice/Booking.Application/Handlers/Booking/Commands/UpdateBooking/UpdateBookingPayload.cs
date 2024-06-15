namespace Booking.Application.Handlers.Booking.Commands.UpdateBooking
{
    public class UpdateBookingPayload
    {
        public string StartDate { get; init; } = default!;
        public string EndDate { get; init; } = default!;
    }
}