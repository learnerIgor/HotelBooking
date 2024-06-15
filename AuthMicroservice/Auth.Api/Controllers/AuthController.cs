using Auth.Application.Dtos;
using Auth.Application.Handlers.Auth.Commands.CreateJwtToken;
using Auth.Application.Handlers.Auth.Commands.CreateJwtTokenByRefreshToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    /// <summary>
    /// AuthController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        /// <summary>
        /// Create jwt token
        /// </summary>
        [HttpPost("/CreateJwtToken")]
        public async Task<JwtTokenDto> CreateJwtToken([FromBody] CreateJwtTokenCommand createJwtTokenCommand, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(createJwtTokenCommand, cancellationToken);
        }

        /// <summary>
        /// Create jwt token by refresh token
        /// </summary>
        [HttpPost("/CreateJwtTokenByRefreshToken")]
        public async Task<JwtTokenDto> RefreshJwtToken([FromBody] CreateJwtTokenByRefreshTokenCommand createJwtTokenByRefreshToken, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(createJwtTokenByRefreshToken, cancellationToken);
        }
    }
}
