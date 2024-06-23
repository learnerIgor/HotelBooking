using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Handlers.External.Hotels;

namespace Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry
{
    public class UpdateCountryCommandHandler : IRequestHandler<UpdateCountryCommand, GetCountryExternalDto>
    {
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCountryCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateCountryCommandHandler(
            IBaseWriteRepository<Country> country,
            IMapper mapper,
            ILogger<UpdateCountryCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _country = country;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetCountryExternalDto> Handle(UpdateCountryCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.CountryId == idGuid, cancellationToken);
            if (country == null)
            {
                throw new NotFoundException($"Country with id {request.Id} doesn't exists.");
            }

            var isCountryExist = await _country.AsAsyncRead().AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (isCountryExist)
            {
                throw new BadOperationException($"Country with name {request.Name} already exists.");
            }

            country.UpdateName(request.Name);

            country = await _country.UpdateAsync(country, cancellationToken);
            _logger.LogInformation($"Country {request.Id} updated.");
            _cleanAccommoCacheService.ClearAllCaches();

            return _mapper.Map<GetCountryExternalDto>(country);
        }
    }
}

