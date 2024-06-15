using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;
using HR.Application.Exceptions;

namespace HR.Application.Handlers.Location.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandHandler : IRequestHandler<CreateCountryCommand, GetCountryDto>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateCountryCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly ICountryProvider _countryProvider;
        private readonly ICurrentUserService _currentUserService;

        public CreateCountryCommandHandler(
            IBaseWriteRepository<Country> country,
            IMapper mapper,
            ILogger<CreateCountryCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            ICountryProvider countryProvider,
            ICurrentUserService currentUserService)
        {
            _country = country;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _countryProvider = countryProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetCountryDto> Handle(CreateCountryCommand request, CancellationToken cancellationToken)
        {
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Name, cancellationToken);
            if(country != null && country.Name == request.Name && country.IsActive)
            {
                throw new BadOperationException($"Country with name {request.Name} already exist");
            }
            if (country != null && !country.IsActive)
            {
                country.UpdateIsActive(true);
                await _country.UpdateAsync(country, cancellationToken);
            }
            else
            {
                country = new Country(request.Name, true);
                await _country.AddAsync(country, cancellationToken);
            }

            await _countryProvider.AddCountryAsync(_currentUserService.Token, country, cancellationToken);
            _logger.LogInformation($"New country {country.Name} created.");
            _cleanHotelRoomCacheService.ClearListCaches();

            return _mapper.Map<GetCountryDto>(country);
        }
    }
}

