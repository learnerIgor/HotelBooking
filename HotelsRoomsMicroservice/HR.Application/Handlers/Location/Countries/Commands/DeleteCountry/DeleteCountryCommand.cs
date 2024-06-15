using MediatR;

namespace HR.Application.Handlers.Location.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
