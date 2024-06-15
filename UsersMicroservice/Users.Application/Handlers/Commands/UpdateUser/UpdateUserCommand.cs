using MediatR;
using Users.Application.Abstractions.Attributes;
using Users.Application.Abstractions.Mappings;
using Users.Application.Dtos;
using Users.Domain;

namespace Users.Application.Handlers.Commands.UpdateUser
{
    [RequestAuthorize]
    public class UpdateUserCommand : IRequest<GetUserDto>, IMapTo<ApplicationUser>
    {
        public string Id { get; init; } = default!;
        public string Login { get; init; } = default!;
        public string Email { get; init; } = default!;
    }
}