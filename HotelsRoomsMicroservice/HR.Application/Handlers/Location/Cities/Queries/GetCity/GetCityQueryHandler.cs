using AutoMapper;
using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Exceptions;
using HR.Domain;

namespace HR.Application.Handlers.Location.Cities.Queries.GetCity
{
    public class GetCityQueryHandler : BaseCashedQuery<GetCityQuery, GetCityDto>
    {
        private readonly IBaseReadRepository<City> _city;
        private readonly IMapper _mapper;

        public GetCityQueryHandler(IBaseReadRepository<City> city, IMapper mapper, ICityMemoryCache memoryCache) : base(memoryCache)
        {
            _city = city;
            _mapper = mapper;
        }

        public async override Task<GetCityDto> SentQueryAsync(GetCityQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(i => i.CityId == idGuid && i.IsActive, cancellationToken);
            if (city == null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetCityDto>(city);
        }
    }
}
