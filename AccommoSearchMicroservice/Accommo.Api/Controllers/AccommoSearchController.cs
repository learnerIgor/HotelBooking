using Accommo.Application.Handlers.Hotels.GetHotels;
using Accommo.Application.Handlers.Hotels.GetHotel;
using Accommo.Application.Handlers.Rooms.GetRoom;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Accommo.Application.Dtos.Hotels;
using Accommo.Application.Dtos.Rooms;
using Accommo.Application.Handlers.Rooms.GetRooms;

namespace Accommo.Api.Controllers
{
    /// <summary>
    /// Accommodation search controller
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccommoSearchController : ControllerBase
    {
        /// <summary>
        /// Get hotels
        /// </summary>
        [HttpGet("/Hotels")]
        public async Task<GetHotelDto[]> GetHotels([FromQuery] GetHotelsQuery query, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        /// <summary>
        /// Get hotel by id
        /// </summary>
        [HttpGet("/Hotel/{id}")]
        public async Task<GetHotelDto> GetHotel([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetHotelQuery { Id = id }, cancellationToken);
        }

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
    }
}
