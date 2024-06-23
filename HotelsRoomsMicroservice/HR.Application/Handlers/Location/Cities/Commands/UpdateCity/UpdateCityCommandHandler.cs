using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Location.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, GetCityDto>
    {
        private readonly IBaseWriteRepository<City> _city;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCityCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly ICityProvider _cityProvider;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCityCommandHandler(
            IBaseWriteRepository<City> city,
            IMapper mapper,
            ILogger<UpdateCityCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            ICityProvider cityProvider,
            ICurrentUserService currentUserService)
        {
            _city = city;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _cityProvider = cityProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetCityDto> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(c => c.CityId == idGuid && c.IsActive, cancellationToken);
            if (city == null)
            {
                throw new NotFoundException($"City with id {request.Id} doesn't exists.");
            }

            var isCityExist = await _city.AsAsyncRead().AnyAsync(c => c.Name == request.Name && c.IsActive, cancellationToken);
            if (isCityExist)
            {
                throw new BadOperationException($"City with name {request.Name} already exists.");
            }

            city.UpdateName(request.Name);

            city = await _city.UpdateAsync(city, cancellationToken);
            await _cityProvider.UpdateCityAsync(_currentUserService.Token, request.Id, city, cancellationToken);
            _logger.LogInformation($"City {city.CityId} updated.");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return _mapper.Map<GetCityDto>(city);
        }
    }
}

