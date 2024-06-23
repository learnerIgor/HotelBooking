using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Location.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, GetCountryDto>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCountryCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly ICountryProvider _countryProvider;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCountryCommandHandler(
            IBaseWriteRepository<Country> country,
            IMapper mapper,
            ILogger<UpdateCountryCommandHandler> logger,
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

        public async Task<GetCountryDto> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.CountryId == idGuid && c.IsActive, cancellationToken);
            if (country == null)
            {
                throw new NotFoundException($"Country with id {request.Id} doesn't exists.");
            }

            var isCountryExist = await _country.AsAsyncRead().AnyAsync(c => c.Name == request.Name && c.IsActive, cancellationToken);
            if (isCountryExist)
            {
                throw new BadOperationException($"Country with name {request.Name} already exists.");
            }

            country.UpdateName(request.Name);

            country = await _country.UpdateAsync(country, cancellationToken);
            await _countryProvider.UpdateCountryAsync(_currentUserService.Token, request.Id, country, cancellationToken);
            _logger.LogInformation($"Country {country.CountryId} updated.");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return _mapper.Map<GetCountryDto>(country);
        }
    }
}

