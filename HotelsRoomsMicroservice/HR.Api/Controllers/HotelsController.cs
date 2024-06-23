using HR.Application.Handlers.Hotels;
using HR.Application.Handlers.Hotels.Commands.CreateHotel;
using HR.Application.Handlers.Hotels.Commands.DeleteHotel;
using HR.Application.Handlers.Hotels.Commands.UpdateHotel;
using HR.Application.Handlers.Hotels.Queries.GetHotel;
using HR.Application.Handlers.Hotels.Queries.GetHotels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.Api.Controllers
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
        /// Get hotel by id
        /// </summary>
        [HttpGet("/Hotels/{id}")]
        public async Task<GetHotelDto> GetHotel([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetHotelQuery { Id = id }, cancellationToken);
        }

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
        /// Create hotel
        /// </summary>
        [HttpPost("/Hotels")]
        public async Task<GetHotelDto> CreateHotel([FromBody] CreateHotelCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);
        }

        /// <summary>
        /// Update hotel by id
        /// </summary>
        [HttpPut("/Hotels/{id}")]
        public async Task<GetHotelDto> UpdateHotel([FromRoute] string id, [FromBody] UpdateHotelPayload payload, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateHotelCommand
            {
                Id = id,
                Name = payload.Name,
                Address = payload.Address,
                IBAN = payload.IBAN,
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
