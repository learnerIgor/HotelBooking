using HR.Application.Handlers.Rooms;
using HR.Application.Handlers.Rooms.Commands.CreateRoom;
using HR.Application.Handlers.Rooms.Commands.DeleteRoom;
using HR.Application.Handlers.Rooms.Commands.UpdateRoom;
using HR.Application.Handlers.Rooms.Queries.GetRoom;
using HR.Application.Handlers.Rooms.Queries.GetRooms;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.Api.Controllers
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
        /// Get room by id
        /// </summary>
        [HttpGet("/Room/{id}")]
        public async Task<GetRoomDto> GetRoom([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetRoomQuery { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Get rooms
        /// </summary>
        [HttpGet("/Rooms")]
        public async Task<GetRoomDto[]> GetRooms([FromQuery] GetRoomsQuery query, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }
        
        /// <summary>
        /// Create room
        /// </summary>
        [HttpPost("/Room")]
        public async Task<GetRoomDto> CreateRoom([FromBody] CreateRoomCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);

        }

        /// <summary>
        /// Update room by id
        /// </summary>
        [HttpPut("/Room/{id}")]
        public async Task<GetRoomDto> UpdateRoom([FromRoute] string id, [FromBody] UpdateRoomPayload roomPayload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var comm = new UpdateRoomCommand(id, roomPayload);
            return await mediator.Send(comm, cancellationToken);
        }

        /// <summary>
        /// Delete room by id
        /// </summary>
        [HttpDelete("/Room/{id}")]
        public Task DeleteRoom([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteRoomCommand { Id = id }, cancellationToken);
        }
    }
}
