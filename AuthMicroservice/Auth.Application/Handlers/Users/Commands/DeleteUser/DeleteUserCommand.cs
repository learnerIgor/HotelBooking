using MediatR;

namespace Auth.Application.Handlers.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; init; } = default!;
    }
}