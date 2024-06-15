using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Rooms;
using MediatR;

namespace Accommo.Application.Handlers.External.Locations.Countries.CreateCountry
{
    public class CreateCountryCommand : IRequest<GetCountryExternalDto>
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = default!;
    }
}
