using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Location.Cities.Commands.CreateCity
{
    public class CreateCityCommand : CommonCommand, IRequest<GetCityDto>
    {
        public string CountryName { get; init; } = default!;
    }
}
