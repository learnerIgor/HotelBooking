using Mail.Application.Handlers.Commands.SendEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mail.Api.Controllers
{
    /// <summary>
    /// EmailController
    /// </summary>
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        /// <summary>
        /// Send email
        /// </summary>
        [HttpPost("/Emails")]
        public async Task<bool> SendEmail([FromBody] SendEmailCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command , cancellationToken);
        }
    }
}
