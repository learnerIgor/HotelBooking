using AutoMapper;
using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Dtos;
using HR.Domain;

namespace HR.Application.Handlers.Location.Cities.Queries.GetCities
{
    internal class GetCitiesQueryHandler : BaseCashedQuery<GetCitiesQuery, BaseListDto<GetCityDto>>
    {
        private readonly IBaseReadRepository<City> _cities;
        private readonly IMapper _mapper;

        public GetCitiesQueryHandler(IBaseReadRepository<City> cities, IMapper mapper, ICityListMemoryCache listMemoryCache) : base(listMemoryCache)
        {
            _cities = cities;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetCityDto>> SentQueryAsync(GetCitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _cities.AsQueryable().Where(c => c.IsActive);

            if(!string.IsNullOrWhiteSpace(request.FreeText))
            {
                query = query.Where(c => c.Name.Contains(request.FreeText) && c.IsActive);
            }

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }
            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            query = query.OrderBy(e => e.CityId);

            var entitiesResult = await _cities.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _cities.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetCityDto[]>(entitiesResult);
            return new BaseListDto<GetCityDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
