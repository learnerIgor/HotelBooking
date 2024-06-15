using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Location.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
    {
        private readonly IBaseWriteRepository<City> _city;
        private readonly ILogger<DeleteCityCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly ICityProvider _cityProvider;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCityCommandHandler(
            IBaseWriteRepository<City> city,
            ILogger<DeleteCityCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            ICityProvider cityProvider,
            ICurrentUserService currentUserService)
        {
            _city = city;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _cityProvider = cityProvider;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(e => e.CityId == idGuid && e.IsActive, cancellationToken);
            if (city is null)
            {
                throw new NotFoundException(request);
            }

            city.UpdateIsActive(false);

            await _city.UpdateAsync(city, cancellationToken);
            await _cityProvider.DeleteCityAsync(_currentUserService.Token, idGuid, cancellationToken);
            _logger.LogWarning($"City {city.CityId} deleted");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return default;
        }
    }
}
