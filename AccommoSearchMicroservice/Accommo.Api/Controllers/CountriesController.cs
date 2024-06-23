using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Locations.Countries.CreateCountry;
using Accommo.Application.Handlers.External.Locations.Countries.DeleteCountry;
using Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accommo.Api.Controllers
{
    /// <summary>
    /// CountriesController
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CountriesController : Controller
    {
        /// <summary>
        /// Create country
        /// </summary>
        [HttpPost("/Countries")]
        public async Task<GetCountryExternalDto> CreateCountry([FromBody] CreateCountryCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);

        }

        /// <summary>
        /// Update country by id
        /// </summary>
        [HttpPatch("/Countries/{id}")]
        public async Task<GetCountryExternalDto> UpdateCountry([FromRoute] string id, [FromBody] UpdateCountryCommandPayLoad payLoad, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var comm = new UpdateCountryCommand { Id = id, Name = payLoad.Name };
            return await mediator.Send(comm, cancellationToken);
        }

        /// <summary>
        /// Delete country by id
        /// </summary>
        [HttpDelete("/Countries/{id}")]
        public Task DeleteCountry([FromRoute] string id, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return mediator.Send(new DeleteCountryCommand { Id = id }, cancellationToken);
        }
    }
}
