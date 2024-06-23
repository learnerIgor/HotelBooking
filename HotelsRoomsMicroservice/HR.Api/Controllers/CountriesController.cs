using HR.Application.Handlers.Location.Countries;
using HR.Application.Handlers.Location.Countries.Commands.CreateCountry;
using HR.Application.Handlers.Location.Countries.Commands.DeleteCountry;
using HR.Application.Handlers.Location.Countries.Commands.UpdateCountry;
using HR.Application.Handlers.Location.Countries.Queries.GetCountries;
using HR.Application.Handlers.Location.Countries.Queries.GetCountry;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HR.Api.Controllers
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
        /// Get country by name
        /// </summary>
        [HttpGet("/Countries/{id}")]
        public async Task<GetCountryDto> GetCountry([FromRoute] string id, IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(new GetCountryQuery { Id = id }, cancellationToken);
        }

        /// <summary>
        /// Get countries
        /// </summary>
        [HttpGet("/Countries")]
        public async Task<GetCountryDto[]> GetCountries([FromQuery] GetCountriesQuery query, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(query, cancellationToken);
            HttpContext.Response.Headers.Append("X-Total-Count", result.TotalCount.ToString());
            return result.Items;
        }

        /// <summary>
        /// Create country
        /// </summary>
        [HttpPost("/Countries")]
        public async Task<GetCountryDto> CreateCountry([FromBody] CreateCountryCommand command, [FromServices] IMediator mediator, CancellationToken cancellationToken)
        {
            return await mediator.Send(command, cancellationToken);

        }

        /// <summary>
        /// Update country by id
        /// </summary>
        [HttpPatch("/Countries/{id}")]
        public async Task<GetCountryDto> UpdateCountry([FromRoute] string id, [FromBody] UpdateCountryCommandPayLoad payLoad, [FromServices] IMediator mediator, CancellationToken cancellationToken)
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
