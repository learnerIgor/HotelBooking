using AutoMapper;
using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Dtos;
using HR.Domain;

namespace HR.Application.Handlers.Location.Countries.Queries.GetCountries
{
    internal class GetCountriesQueryHandler : BaseCashedQuery<GetCountriesQuery, BaseListDto<GetCountryDto>>
    {
        private readonly IBaseReadRepository<Country> _countries;
        private readonly IMapper _mapper;

        public GetCountriesQueryHandler(IBaseReadRepository<Country> countries, IMapper mapper, ICountryListMemoryCache listMemoryCache) : base(listMemoryCache)
        {
            _countries = countries;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetCountryDto>> SentQueryAsync(GetCountriesQuery request, CancellationToken cancellationToken)
        {
            var query = _countries.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.FreeText))
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

            query = query.OrderBy(e => e.CountryId);

            var entitiesResult = await _countries.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _countries.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetCountryDto[]>(entitiesResult);
            return new BaseListDto<GetCountryDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
