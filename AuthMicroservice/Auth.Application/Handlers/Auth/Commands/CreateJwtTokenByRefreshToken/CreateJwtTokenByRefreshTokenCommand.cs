using Auth.Application.Dtos;
using MediatR;

namespace Auth.Application.Handlers.Auth.Commands.CreateJwtTokenByRefreshToken
{
    public class CreateJwtTokenByRefreshTokenCommand : IRequest<JwtTokenDto>
    {
        public string RefreshToken { get; init; } = default!;
    }
}
