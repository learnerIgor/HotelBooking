using MediatR;

namespace HR.Application.Handlers.Location.Cities.Queries.GetCity
{
    public class GetCityQuery : IRequest<GetCityDto>
    {
        public string Id { get; init; } = default!;
    }
}
