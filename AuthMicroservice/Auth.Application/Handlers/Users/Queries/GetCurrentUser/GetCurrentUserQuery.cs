using Auth.Application.Dtos;
using MediatR;

namespace Auth.Application.Handlers.Users.Queries.GetCurrentUser
{
    public class GetCurrentUserQuery : IRequest<GetUserDto>;
}