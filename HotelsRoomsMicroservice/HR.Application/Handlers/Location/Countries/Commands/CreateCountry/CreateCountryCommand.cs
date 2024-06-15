using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Location.Countries.Commands.CreateCountry
{
    public class CreateCountryCommand : CommonCommand, IRequest<GetCountryDto>
    {
    }
}
