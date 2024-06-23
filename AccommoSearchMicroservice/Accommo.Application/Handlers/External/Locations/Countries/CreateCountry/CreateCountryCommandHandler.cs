using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Handlers.External.Hotels;

namespace Accommo.Application.Handlers.External.Locations.Countries.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, GetCountryExternalDto>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCountryCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public CreateCountryCommandHandler(
            IBaseWriteRepository<Country> country,
            IMapper mapper,
            ILogger<CreateCountryCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _country = country;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetCountryExternalDto> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Name, cancellationToken);
            if (country != null && !country.IsActive)
            {
                country.UpdateIsActive(true);
                await _country.UpdateAsync(country, cancellationToken);
            }
            else
            {
                country = new Country(request.CountryId, request.Name, true);
                await _country.AddAsync(country, cancellationToken);
            }

            _logger.LogInformation($"New country {country.Name} created in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearListCaches();

            return _mapper.Map<GetCountryExternalDto>(country);
        }
    }
}

