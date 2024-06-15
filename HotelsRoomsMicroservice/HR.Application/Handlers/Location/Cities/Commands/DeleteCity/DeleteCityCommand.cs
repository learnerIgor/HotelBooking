using MediatR;

namespace HR.Application.Handlers.Location.Cities.Commands.DeleteCity
{
    public class DeleteCityCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
