using MediatR;
using Users.Application.Abstractions.Attributes;

namespace Users.Application.Handlers.Commands.DeleteUser
{
    [RequestAuthorize]
    public class DeleteUserCommand : IRequest<Unit>
    {
        public string Id { get; init; } = default!;
    }
}