using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.Locations.Cities.DeleteCity
{
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, Unit>
    {
        private readonly IBaseWriteRepository<City> _city;
        private readonly ILogger<DeleteCityCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public DeleteCityCommandHandler(
            IBaseWriteRepository<City> city,
            ILogger<DeleteCityCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _city = city;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<Unit> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(e => e.CityId == idGuid, cancellationToken);
            if (city is null)
            {
                throw new NotFoundException(request);
            }

            city.UpdateIsActive(false);

            await _city.UpdateAsync(city, cancellationToken);
            _logger.LogWarning($"City {city.CityId} deleted in AccommoMicroservice");
            _cleanAccommoCacheService.ClearAllCaches();

            return default;
        }
    }
}
