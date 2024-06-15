using Accommo.Domain;
using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.Hotels.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, GetHotelExternalDto>
    {
        private readonly IBaseWriteRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IBaseWriteRepository<City> _city;
        private readonly IBaseWriteRepository<Address> _address;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateHotelCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanaccommoCacheService;

        public CreateHotelCommandHandler(
            IBaseWriteRepository<Hotel> hotel,
            IBaseWriteRepository<Country> country,
            IBaseWriteRepository<City> city,
            IBaseWriteRepository<Address> address,
            IMapper mapper,
            ILogger<CreateHotelCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _hotel = hotel;
            _country = country;
            _city = city;
            _address = address;
            _mapper = mapper;
            _logger = logger;
            _cleanaccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetHotelExternalDto> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Address.City.Country.Name && c.IsActive, cancellationToken);
            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Address.City.Name && c.IsActive, cancellationToken);
            
            var address = await _address.AsAsyncRead().SingleOrDefaultAsync(c => c.Latitude == request.Address.Latitude
                                                                                && c.Longitude == request.Address.Longitude
                                                                                && c.Street == request.Address.Street
                                                                                && c.HouseNumber == request.Address.HouseNumber
                                                                                && c.IsActive
                                                                                , cancellationToken);
            if (address == null)
            {
                address = new Address(request.AddressId, request.Address.Street, request.Address.HouseNumber, request.Address.Latitude, request.Address.Longitude, true, city!.CityId);
                await _address.AddAsync(address, cancellationToken);
            }

            var hotel = new Hotel(request.HotelId, request.Name, request.AddressId, request.Description,  request.Rating, true, request.Image);

            hotel = await _hotel.AddAsync(hotel, cancellationToken);
            _logger.LogInformation($"New hotel {hotel.HotelId} created.");
            _cleanaccommoCacheService.ClearListCaches();

            return _mapper.Map<GetHotelExternalDto>(hotel);
        }
    }
}

