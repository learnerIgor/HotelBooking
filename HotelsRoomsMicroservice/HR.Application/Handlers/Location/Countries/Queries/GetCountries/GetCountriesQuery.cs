using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Location.Countries.Queries.GetCountries
{
    public class GetCountriesQuery : ListFilter, IBasePaginationFilter, IRequest<BaseListDto<GetCountryDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
