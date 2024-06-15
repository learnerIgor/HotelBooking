namespace Booking.Application.Handlers.User.UpdateUser
{
    public class UpdateUserPayload
    {
        public required string Login { get; init; } = default!;
        public required string Email { get; init; } = default!;
    }
}