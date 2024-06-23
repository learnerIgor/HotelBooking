using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Hotels.CreateHotel;
using Accommo.Application.Handlers.External.Hotels.DeleteHotel;
using Accommo.Application.Handlers.External.Hotels.UpdateHotel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommo.Api.Controllers
{
    /// <summary>
    /// HotelController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class HotelsController : Controller
    {
        /// <summary>
        /// Create hotel
        /// </summary>
        [HttpPost("/Hotels")]
        public async Task<GetHotelExternalDto> CreateHotel([FromBody] CreateHotelCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Update hotel by id
        /// </summary>
        [HttpPut("/Hotels/{id}")]
        public async Task<GetHotelExternalDto> UpdateHotel([FromRoute] string id, [FromBody] UpdateHotelPayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateHotelCommand
            {
                Id = id,
                Name = payload.Name,
                Address = payload.Address,
                AddressId = payload.AddressId,
                Rating = payload.Rating,
                Image = payload.Image,
                Description = payload.Description,
            }, cancellationToken);
        }

        /// <summary>
        /// Delete hotel by id
        /// </summary>
        [HttpDelete("/Hotels/{id}")]
        public Task DeleteHotel([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteHotelCommand { Id = id }, cancellationToken);
        }
    }
}
