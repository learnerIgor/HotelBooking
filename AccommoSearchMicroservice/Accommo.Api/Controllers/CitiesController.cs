using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Locations.Cities.CreateCity;
using Accommo.Application.Handlers.External.Locations.Cities.DeleteCity;
using Accommo.Application.Handlers.External.Locations.Cities.UpdateCity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommo.Api.Controllers
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
        /// Create city
        /// </summary>
        [HttpPost("/Cities")]
        public async Task<GetCityExternalDto> CreateCity([AsParameters] string countryName, [FromBody] CreateCityCommandPayLoad command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new CreateCityCommand { CountryName = countryName, CityName = command.Name, CityId = command.CityId }, cancellationToken);

        }

        /// <summary>
        /// Update city by id
        /// </summary>
        [HttpPatch("/Cities/{id}")]
        public async Task<GetCityExternalDto> UpdateCity([FromRoute] string id, [FromBody] UpdateCityCommandPayLoad payLoad, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new UpdateCityCommand { Id = id, Name = payLoad.Name }, cancellationToken);
        }

        /// <summary>
        /// Delete city by id
        /// </summary>
        [HttpDelete("/Cities/{id}")]
        public Task DeleteCity([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteCityCommand { Id = id }, cancellationToken);
        }
    }
}
