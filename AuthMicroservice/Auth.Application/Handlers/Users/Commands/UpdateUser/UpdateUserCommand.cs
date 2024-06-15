using Auth.Application.Abstractions.Mappings;
using Auth.Domain;
using MediatR;

namespace Auth.Application.Handlers.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<UserCommandDto>, IMapTo<ApplicationUser>
    {
        public string Id { get; init; } = default!;
        public string Login { get; init; } = default!;
    }
}