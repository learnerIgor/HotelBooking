using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Location.Cities.Queries.GetCities
{
    public class GetCitiesQuery : ListFilter, IBasePaginationFilter, IRequest<BaseListDto<GetCityDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
