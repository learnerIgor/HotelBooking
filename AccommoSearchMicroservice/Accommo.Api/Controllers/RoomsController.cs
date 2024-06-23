using Accommo.Application.Handlers.External.Rooms;
using Accommo.Application.Handlers.External.Rooms.CreateRoom;
using Accommo.Application.Handlers.External.Rooms.DeleteRoom;
using Accommo.Application.Handlers.External.Rooms.GetRoomById;
using Accommo.Application.Handlers.External.Rooms.UpdateRoom;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommo.Api.Controllers
{
    /// <summary>
    /// RoomController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoomsController : Controller
    {
        /// <summary>
        /// Create room
        /// </summary>
        [HttpPost("/Rooms")]
        public async Task<GetRoomExternalDto> CreateRoom([FromBody] CreateRoomCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);

        }

        /// <summary>
        /// Update room by id
        /// </summary>
        [HttpPut("/Rooms/{id}")]
        public async Task<GetRoomExternalDto> UpdateRoom([FromRoute] string id, [FromBody] UpdateRoomPayload roomPayload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var comm = new UpdateRoomCommand(id, roomPayload);
            return await mediator.Send(comm, cancellationToken);
        }

        /// <summary>
        /// Delete room by id
        /// </summary>
        [HttpDelete("/Rooms/{id}")]
        public Task DeleteRoom([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteRoomCommand { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Get room by id
        /// </summary>
        [AllowAnonymous]
        [HttpGet("/RoomsBook/{id}")]
        public async Task<GetRoomBookDto> GetRoomBook([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetRoomByIdQuery { Id = id }, cancellationToken);
        }
    }
}
