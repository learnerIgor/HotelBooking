using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.Locations.Countries.DeleteCountry
{
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, Unit>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly ILogger<DeleteCountryCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public DeleteCountryCommandHandler(
            IBaseWriteRepository<Country> country,
            ILogger<DeleteCountryCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _country = country;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<Unit> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(e => e.CountryId == idGuid, cancellationToken);
            if (country is null)
            {
                throw new NotFoundException(request);
            }

            country.UpdateIsActive(false);

            await _country.UpdateAsync(country, cancellationToken);
            _logger.LogWarning($"Country {country.CountryId} deleted from AccommoMicroservice");
            _cleanAccommoCacheService.ClearAllCaches();

            return default;
        }
    }
}
