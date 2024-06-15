using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Location.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommand : CommonCommand, IRequest<GetCountryDto>
    {
        public string Id { get; init; } = default!;
    }
}
