using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Rooms;
using MediatR;

namespace Accommo.Application.Handlers.External.Locations.Cities.CreateCity
{
    public class CreateCityCommand : IRequest<GetCityExternalDto>
    {
        public string CityId { get; set; } = default!;
        public string CityName { get; init; } = default!;
        public string CountryName { get; init; } = default!;
    }
}
