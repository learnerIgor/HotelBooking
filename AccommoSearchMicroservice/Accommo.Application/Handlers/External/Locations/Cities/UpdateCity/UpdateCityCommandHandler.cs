using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Handlers.External.Hotels;

namespace Accommo.Application.Handlers.External.Locations.Cities.UpdateCity
{
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, GetCityExternalDto>
    {
        private readonly IBaseWriteRepository<City> _city;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateCityCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateCityCommandHandler(
            IBaseWriteRepository<City> city,
            IMapper mapper,
            ILogger<UpdateCityCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _city = city;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetCityExternalDto> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(c => c.CityId == idGuid, cancellationToken);
            if (city == null)
            {
                throw new NotFoundException($"City with id {request.Id} doesn't exists.");
            }

            var isCityExist = await _city.AsAsyncRead().AnyAsync(c => c.Name == request.Name, cancellationToken);
            if (isCityExist)
            {
                throw new BadOperationException($"City with name {request.Name} already exists.");
            }

            city.UpdateName(request.Name);

            city = await _city.UpdateAsync(city, cancellationToken);
            _logger.LogInformation($"City {request.Id} updated.");
            _cleanAccommoCacheService.ClearAllCaches();

            return _mapper.Map<GetCityExternalDto>(city);
        }
    }
}

