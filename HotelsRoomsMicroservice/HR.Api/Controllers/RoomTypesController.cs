using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HR.Application.Handlers.RoomTypes;
using HR.Application.Handlers.RoomTypes.Commands.CreateRoomType;
using HR.Application.Handlers.RoomTypes.Commands.DeleteRoomType;
using HR.Application.Handlers.RoomTypes.Commands.UpdateRoomType;
using HR.Application.Handlers.RoomTypes.Commands.UpdateRoomTypeCost;
using HR.Application.Handlers.RoomTypes.Queries.GetRoomType;
using HR.Application.Handlers.RoomTypes.Queries.GetRoomTypes;
using MediatR;

namespace HR.Api.Controllers
{
    /// <summary>
    /// RoomTypesController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RoomTypesController : Controller
    {
        /// <summary>
        /// Get type of room by id
        /// </summary>
        [HttpGet("/RoomTypes/{id}")]
        public async Task<GetRoomTypeDto> GetRoomType([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetRoomTypeQuery { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Get room types
        /// </summary>
        [HttpGet("/RoomTypes")]
        public async Task<GetRoomTypeDto[]> GetRoomTypes([FromQuery] GetRoomTypesQuery query, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        /// <summary>
        /// Create room type
        /// </summary>
        [HttpPost("/RoomTypes")]
        public async Task<GetRoomTypeDto> CreateRoomType([FromBody] CreateRoomTypeCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Update room type by id
        /// </summary>
        [HttpPut("/RoomTypes/{id}")]
        public async Task<GetRoomTypeDto> UpdateRoomType([FromRoute] string id, [FromBody] UpdateRoomTypePayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateRoomTypeCommand
            {
                Id = id,
                Name = payload.Name,
            }, cancellationToken);
        }

        /// <summary>
        /// Delete room type by id
        /// </summary>
        [HttpDelete("/RoomTypes/{id}")]
        public Task DeleteRoomType([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteRoomTypeCommand { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Update cost of room type by id
        /// </summary>
        [HttpPatch("/RoomTypes/{id}/Cost")]
        public async Task<GetRoomTypeDto> UpdateRoomTypeCost([FromRoute] string id, [FromBody] UpdateRoomTypeCostPayload payload, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateRoomTypeCostCommand { Id = id, BaseCost = payload.BaseCost }, cancellationToken);
        }
    }
}
