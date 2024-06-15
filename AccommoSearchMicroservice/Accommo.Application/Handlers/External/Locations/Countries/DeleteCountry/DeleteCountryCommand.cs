using MediatR;

namespace Accommo.Application.Handlers.External.Locations.Countries.DeleteCountry
{
    public class DeleteCountryCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
