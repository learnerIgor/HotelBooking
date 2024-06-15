using Microsoft.AspNetCore.Mvc;
using MediatR;
using Accommo.Application.Handlers.External.RoomTypes.CreateRoomType;
using Accommo.Application.Handlers.External.RoomTypes;
using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType;
using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomTypeCost;
using Accommo.Application.Handlers.External.RoomTypes.DeleteRoomType;
using Microsoft.AspNetCore.Authorization;

namespace Accommo.Api.Controllers
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
        /// Create room type
        /// </summary>
        [HttpPost("/RoomType")]
        public async Task<GetRoomTypeExternalDto> CreateRoomType([FromBody] CreateRoomTypeCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Update room type by id
        /// </summary>
        [HttpPut("/RoomType/{id}")]
        public async Task<GetRoomTypeExternalDto> UpdateRoomType([FromRoute] string id, [FromBody] UpdateRoomTypePayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
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
        [HttpDelete("/RoomType/{id}")]
        public Task DeleteRoomType([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteRoomTypeCommand { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Update cost of room type by id
        /// </summary>
        [HttpPatch("/RoomType/{id}/Cost")]
        public async Task<GetRoomTypeExternalDto> UpdateRoomTypeCost([FromRoute] string id, [FromBody] UpdateRoomTypeCostPayload payload, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateRoomTypeCostCommand { Id = id, BaseCost = payload.BaseCost }, cancellationToken);
        }
    }
}
