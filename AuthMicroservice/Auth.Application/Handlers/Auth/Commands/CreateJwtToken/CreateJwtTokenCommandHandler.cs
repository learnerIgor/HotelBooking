using Auth.Application.Abstractions.ExternalProviders;
using Auth.Application.Abstractions.Persistence.Repositories.Write;
using Auth.Application.Dtos;
using Auth.Application.Exceptions;
using Auth.Application.Services;
using Auth.Application.Utils;
using Auth.Domain;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Auth.Application.Handlers.Auth.Commands.CreateJwtToken
{
    public class CreateJwtTokenCommandHandler : IRequestHandler<CreateJwtTokenCommand, JwtTokenDto>
    {
        private readonly IBaseWriteRepository<RefreshToken> _refreshTokens;
        private readonly ICreateJwtTokenService _createJwtTokenService;
        private readonly IConfiguration _configuration;
        private readonly IUsersProvider _usersProvider;

        public CreateJwtTokenCommandHandler(
            IBaseWriteRepository<RefreshToken> _refreshToken,
            IConfiguration configuration,
            ICreateJwtTokenService createJwtTokenService,
            IUsersProvider usersProvider)
        {
            _refreshTokens = _refreshToken;
            _configuration = configuration;
            _createJwtTokenService = createJwtTokenService;
            _usersProvider = usersProvider;
        }
        public async Task<JwtTokenDto> Handle(CreateJwtTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _usersProvider.GetUserAsync(request.Login, cancellationToken);

            if (!PasswordHashUtil.Verify(request.Password, user.PasswordHash))
            {
                throw new ForbiddenException();
            }

            var jwtTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:JwtToken"]!));
            var refreshTokenDateExpires = DateTime.UtcNow.AddSeconds(int.Parse(_configuration["TokensLifeTime:RefreshToken"]!));
            var token = _createJwtTokenService.CreateJwtToken(user, jwtTokenDateExpires);
            var refreshToken = await _refreshTokens.AddAsync(new RefreshToken(user.ApplicationUserId, refreshTokenDateExpires), cancellationToken);

            return new JwtTokenDto
            {
                JwtToken = token,
                RefreshToken = refreshToken.RefreshTokenId.ToString(),
                Expires = jwtTokenDateExpires
            };
        }
    }
}
