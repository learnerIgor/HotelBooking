using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Rooms;
using MediatR;

namespace Accommo.Application.Handlers.External.Locations.Cities.UpdateCity
{
    public class UpdateCityCommand : IRequest<GetCityExternalDto>
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
    }
}
