using Auth.Application.Dtos;
using Auth.Application.Handlers.Users.Commands;
using Auth.Application.Handlers.Users.Commands.DeleteUser;
using Auth.Application.Handlers.Users.Commands.UpdateUser;
using Auth.Application.Handlers.Users.Commands.UpdateUserPassword;
using Auth.Application.Handlers.Users.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Api.Controllers
{
    /// <summary>
    /// UsersController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Get current user
        /// </summary>
        [Authorize]
        [HttpGet("/CurrentUser")]
        public async Task<GetUserDto> GetCurrentUser(IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCurrentUserQuery(), cancellationToken);
        }

        /// <summary>
        /// Delete user by Mq
        /// </summary>
        [AllowAnonymous]
        [HttpDelete("/DeleteUser/{id}")]
        public Task DeleteUser([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Update user by Mq
        /// </summary>
        [AllowAnonymous]
        [HttpPut("/UpdateUser/{id}")]
        public async Task<UserCommandDto> PutUser([FromServices] IMediator mediator, [FromRoute] string id, [FromBody] UpdateUserPayload payload, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateUserCommand
            {
                Id = id,
                Login = payload.Login,
            }, cancellationToken);
        }

        /// <summary>
        /// Update user password by Mq
        /// </summary>
        [AllowAnonymous]
        [HttpPatch("/UpdatePassword/{id}")]
        public Task PatchUserPassword([FromServices] IMediator mediator, [FromRoute] string id, [FromBody] UpdateUserPasswordPayload payload, CancellationToken cancellationToken)
        {
            var command = new UpdateUserPasswordCommand()
            {
                UserId = id,
                PasswordHash = payload.PasswordHash,
            };
            return mediator.Send(command, cancellationToken);
        }
    }
}
