using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Abstractions.Service;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HR.Application.Handlers.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandHandler : IRequestHandler<CreateHotelCommand, GetHotelDto>
    {
        private readonly IBaseWriteRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IBaseWriteRepository<City> _city;
        private readonly IBaseWriteRepository<Address> _address;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateHotelCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IHotelProvider _hotelRoomProvider;
        private readonly ICurrentUserService _currentUserService;

        public CreateHotelCommandHandler(
            IBaseWriteRepository<Hotel> hotel,
            IBaseWriteRepository<Country> country,
            IBaseWriteRepository<City> city,
            IBaseWriteRepository<Address> address,
            IMapper mapper,
            ILogger<CreateHotelCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IHotelProvider hotelRoomProvider,
            ICurrentUserService currentUserService)
        {
            _hotel = hotel;
            _country = country;
            _city = city;
            _address = address;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _hotelRoomProvider = hotelRoomProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetHotelDto> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        { 
            var isHotelExist = await _hotel.AsAsyncRead().AnyAsync(e => e.Name == request.Name
                                                                                && e.IsActive
                                                                                && e.Address.Street == request.Address.Street
                                                                                && e.Address.HouseNumber == request.Address.HouseNumber
                                                                                && e.Address.Latitude == request.Address.Latitude
                                                                                && e.Address.Longitude == request.Address.Longitude
                                                                                && e.Address.IsActive
                                                                                , cancellationToken);

            if (isHotelExist)
            {
                throw new BadOperationException($"Hotel with name {request.Name} and such address already exist.");
            }

            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Address.City.Name && c.IsActive, cancellationToken);
            if (city == null)
            {
                throw new BadOperationException($"City with name {request.Address.City.Name} doesn't exist.");
            }

            var country = await _country.AsAsyncRead().SingleOrDefaultAsync(c => c.Name == request.Address.City.Country.Name && c.IsActive, cancellationToken);
            if (country == null)
            {
                throw new BadOperationException($"Country with name {request.Address.City.Country.Name} doesn't exist.");
            }
            if (country!.Name != city.Country.Name)
            {
                throw new BadOperationException($"City {request.Address.City.Name} can't be create in country {request.Address.City.Country.Name}");
            }

            var address = await _address.AsAsyncRead().SingleOrDefaultAsync(c => c.Latitude == request.Address.Latitude
                                                                                && c.Longitude == request.Address.Longitude
                                                                                && c.Street == request.Address.Street
                                                                                && c.HouseNumber == request.Address.HouseNumber
                                                                                && c.IsActive
                                                                                , cancellationToken);

            if (address == null)
            {
                address = new Address(request.Address.Street, request.Address.HouseNumber, request.Address.Latitude, request.Address.Longitude, true, city.CityId);
                await _address.AddAsync(address, cancellationToken);
            }
            else
            {
                throw new BadOperationException($"Address already exist.");
            }

            var hotel = new Hotel(request.Name, address.AddressId, request.Description, request.IBAN, request.Rating, true, request.Image);

            hotel = await _hotel.AddAsync(hotel, cancellationToken);
            await _hotelRoomProvider.AddHotelAsync(_currentUserService.Token, hotel, cancellationToken);
            _logger.LogInformation($"New hotel {hotel.HotelId} created.");
            _cleanHotelRoomCacheService.ClearListCaches();

            return _mapper.Map<GetHotelDto>(hotel);
        }
    }
}

