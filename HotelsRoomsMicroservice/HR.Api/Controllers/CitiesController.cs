using HR.Application.Handlers.Location.Cities;
using HR.Application.Handlers.Location.Cities.Commands.CreateCity;
using HR.Application.Handlers.Location.Cities.Commands.DeleteCity;
using HR.Application.Handlers.Location.Cities.Commands.UpdateCity;
using HR.Application.Handlers.Location.Cities.Queries.GetCities;
using HR.Application.Handlers.Location.Cities.Queries.GetCity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.Api.Controllers
{
    /// <summary>
    /// CitiesController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CitiesController : Controller
    {
        /// <summary>
        /// Get city by name
        /// </summary>
        [HttpGet("/City/{id}")]
        public async Task<GetCityDto> GetCity([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCityQuery { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Get cities
        /// </summary>
        [HttpGet("/Cities")]
        public async Task<GetCityDto[]> GetCities([FromQuery] GetCitiesQuery query, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        /// <summary>
        /// Create city
        /// </summary>
        [HttpPost("/City")]
        public async Task<GetCityDto> CreateCity([AsParameters] string countryName, [FromBody] CreateCityCommandPayLoad command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateCityCommand { CountryName = countryName, Name = command.Name }, cancellationToken);

        }

        /// <summary>
        /// Update city by id
        /// </summary>
        [HttpPatch("/City/{id}")]
        public async Task<GetCityDto> UpdateCity([FromRoute] string id, [FromBody] UpdateCityCommandPayLoad payLoad, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var comm = new UpdateCityCommand { Id = id, Name = payLoad.Name };
            return await mediator.Send(comm, cancellationToken);
        }

        /// <summary>
        /// Delete city by id
        /// </summary>
        [HttpDelete("/City/{id}")]
        public Task DeleteCity([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteCityCommand { Id = id }, cancellationToken);
        }
    }
}
