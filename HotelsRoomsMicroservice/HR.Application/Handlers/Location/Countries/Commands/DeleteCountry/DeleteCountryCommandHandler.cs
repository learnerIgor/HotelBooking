using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Location.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Unit>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly ILogger<DeleteCountryCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly ICountryProvider _countryProvider;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCountryCommandHandler(
            IBaseWriteRepository<Country> country,
            ILogger<DeleteCountryCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            ICountryProvider countryProvider,
            ICurrentUserService currentUserService)
        {
            _country = country;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _countryProvider = countryProvider;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(e => e.CountryId == idGuid && e.IsActive, cancellationToken);
            if (country is null)
            {
                throw new NotFoundException(request);
            }

            country.UpdateIsActive(false);

            await _country.UpdateAsync(country, cancellationToken);
            await _countryProvider.DeleteCountryAsync(_currentUserService.Token, idGuid, cancellationToken);
            _logger.LogWarning($"Country {country.CountryId} deleted");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return default;
        }
    }
}
