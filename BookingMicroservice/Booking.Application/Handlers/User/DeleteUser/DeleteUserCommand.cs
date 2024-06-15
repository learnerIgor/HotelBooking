using MediatR;

namespace Booking.Application.Handlers.User.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; init; } = default!;
    }
}