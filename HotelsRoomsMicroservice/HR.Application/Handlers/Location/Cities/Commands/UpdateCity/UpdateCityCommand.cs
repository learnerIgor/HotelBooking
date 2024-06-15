using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Location.Cities.Commands.UpdateCity
{
    public class UpdateCityCommand : CommonCommand, IRequest<GetCityDto>
    {
        public string Id { get; init; } = default!;
    }
}
