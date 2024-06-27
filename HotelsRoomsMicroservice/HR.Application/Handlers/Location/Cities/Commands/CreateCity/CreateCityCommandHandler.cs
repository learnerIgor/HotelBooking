using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Location.Cities.Commands.CreateCity
{
    public class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, GetCityDto>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IBaseWriteRepository<City> _city;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCityCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly ICityProvider _cityProvider;
        private readonly ICurrentUserService _currentUserService;

        public CreateCityCommandHandler(
            IBaseWriteRepository<Country> country,
            IBaseWriteRepository<City> city,
            IMapper mapper,
            ILogger<CreateCityCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            ICityProvider cityProvider,
            ICurrentUserService currentUserService)
        {
            _country = country;
            _city = city;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _cityProvider = cityProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetCityDto> Handle(CreateCityCommand request, CancellationToken cancellationToken)
        {
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.CountryName && c.IsActive, cancellationToken);
            if (country == null)
            {
                throw new NotFoundException($"Country with name {request.CountryName} doesn't exists.");
            }

            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Name, cancellationToken);
            if(city != null && city.Name == request.Name && city.IsActive)
            {
                throw new BadOperationException($"City with name {request.Name} exists.");
            }
            if (city != null && !city.IsActive)
            {
                city.UpdateIsActive(true);
                await _city.UpdateAsync(city, cancellationToken);
            }
            else
            {
                city = new City(request.Name, country.CountryId, true);
                await _city.AddAsync(city, cancellationToken);
            }

            await _cityProvider.AddCityAsync(_currentUserService.Token, request.CountryName, city, cancellationToken);
            _logger.LogInformation($"New hotel {city.CityId} created.");
            _cleanHotelRoomCacheService.ClearListCaches();

            return _mapper.Map<GetCityDto>(city);
        }
    }
}

