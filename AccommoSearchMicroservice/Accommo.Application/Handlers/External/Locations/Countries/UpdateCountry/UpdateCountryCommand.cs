using Accommo.Application.Handlers.External.Hotels;
using MediatR;

namespace Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry
{
    public class UpdateCountryCommand : IRequest<GetCountryExternalDto>
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
    }
}
