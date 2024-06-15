namespace Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingPayload
    {
        public string CheckInDate { get; init; } = default!;
        public string CheckOutDate { get; init; } = default!;
    }
}