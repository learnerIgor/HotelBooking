namespace Booking.Application.Handlers.Booking.Commands
{
    public class SendEmailDto
    {
        public string ApplicationUserId { get; init; } = default!;
        public string Login { get; init; } = default!;
        public string Email { get; init; } = default!;
        public string Hotel { get; init; } = default!;
        public string RoomType { get; init; } = default!;
        public string CheckIn { get; init; } = default!;
        public string CheckOut { get; init; } = default!;
    }
}
