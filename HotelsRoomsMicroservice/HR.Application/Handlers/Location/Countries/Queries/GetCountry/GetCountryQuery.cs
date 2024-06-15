using MediatR;

namespace HR.Application.Handlers.Location.Countries.Queries.GetCountry
{
    public class GetCountryQuery : IRequest<GetCountryDto>
    {
        public string Id { get; init; } = default!;
    }
}
