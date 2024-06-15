using Booking.Application.Handlers.User;
using Booking.Application.Handlers.User.CreateUser;
using Booking.Application.Handlers.User.DeleteUser;
using Booking.Application.Handlers.User.UpdateUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.Api.Controllers
{
    /// <summary>
    /// UsersController
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        /// <summary>
        /// Add user by Mq
        /// </summary>
        [AllowAnonymous]
        [HttpPost("/User")]
        public async Task<GetUserDto> AddUser([FromBody] CreateUserCommand command, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Delete user by Mq
        /// </summary>
        [AllowAnonymous]
        [HttpDelete("/DeleteUser/{id}")]
        public Task DeleteUser([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteUserCommand{ Id = id }, cancellationToken);
        }

        /// <summary>
        /// Update user by Mq
        /// </summary>
        [AllowAnonymous]
        [HttpPut("/UpdateUser/{id}")]
        public async Task<GetUserDto> PutUser([FromServices] IMediator mediator, [FromRoute] string id, [FromBody] UpdateUserPayload payload, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateUserCommand
            {
                Id = id,
                Login = payload.Login,
                Email = payload.Email
            }, cancellationToken);
        }
    }
}
