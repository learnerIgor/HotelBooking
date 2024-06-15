using AutoMapper;
using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Exceptions;
using HR.Domain;

namespace HR.Application.Handlers.Location.Countries.Queries.GetCountry
{
    public class GetCountryQueryHandler : BaseCashedQuery<GetCountryQuery, GetCountryDto>
    {
        private readonly IBaseReadRepository<Country> _city;
        private readonly IMapper _mapper;

        public GetCountryQueryHandler(IBaseReadRepository<Country> city, IMapper mapper, ICountryMemoryCache memoryCache) : base(memoryCache)
        {
            _city = city;
            _mapper = mapper;
        }

        public async override Task<GetCountryDto> SentQueryAsync(GetCountryQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(i => i.CountryId == idGuid && i.IsActive, cancellationToken);
            if (city == null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetCountryDto>(city);
        }
    }
}
