using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Rooms;

namespace Accommo.Application.Handlers.External.Locations.Cities.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, GetCityExternalDto>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IBaseWriteRepository<City> _city;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCityCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public CreateCityCommandHandler(
            IBaseWriteRepository<Country> country,
            IBaseWriteRepository<City> city,
            IMapper mapper,
            ILogger<CreateCityCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _country = country;
            _city = city;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetCityExternalDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.CountryName, cancellationToken);
            if (country == null)
            {
                throw new BadOperationException($"Country with name {request.CountryName} doesn't exists in AccommoMicroservice.");
            }

            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.CityName, cancellationToken);
            if (city != null && !city.IsActive)
            {
                city.UpdateIsActive(true);
                await _city.AddAsync(city, cancellationToken);
            }
            else
            {
                city = new City(Guid.Parse(request.CityId), request.CityName, country.CountryId, true);
                await _city.AddAsync(city, cancellationToken);
            }


            _logger.LogInformation($"New hotel {city.CityId} created in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearListCaches();

            return _mapper.Map<GetCityExternalDto>(city);
        }
    }
}

