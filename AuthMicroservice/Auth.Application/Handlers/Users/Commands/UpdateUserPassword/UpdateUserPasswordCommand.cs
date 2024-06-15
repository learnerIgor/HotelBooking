using MediatR;

namespace Auth.Application.Handlers.Users.Commands.UpdateUserPassword
{
    public class UpdateUserPasswordCommand : IRequest
    {
        public string UserId { get; init; } = default!;

        public string PasswordHash { get; init; } = default!;
    }
}