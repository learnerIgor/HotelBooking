using MediatR;

namespace Accommo.Application.Handlers.External.Locations.Cities.DeleteCity
{
    public class DeleteCityCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
