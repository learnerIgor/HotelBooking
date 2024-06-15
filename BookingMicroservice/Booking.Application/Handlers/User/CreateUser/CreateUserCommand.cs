using MediatR;

namespace Booking.Application.Handlers.User.CreateUser
{
    public class CreateUserCommand : IRequest<GetUserDto>
    {
        public string ApplicationUserId { get; set; } = default!;
        public string Login { get; init; } = default!;
        public bool IsActive { get; init; }
        public string Email { get; set; } = default!;
    }
}
