using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Exceptions;
using HR.Application.Caches;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, GetHotelDto>
    {
        private readonly IBaseWriteRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<Country> _country;
        private readonly IBaseWriteRepository<City> _city;
        private readonly IBaseWriteRepository<Address> _address;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateHotelCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IHotelProvider _hotelProvider;
        private readonly ICurrentUserService _currentUserService;

        public UpdateHotelCommandHandler(
            IBaseWriteRepository<Hotel> hotel, 
            IBaseWriteRepository<Country> country, 
            IBaseWriteRepository<City> city, 
            IBaseWriteRepository<Address> address, 
            IMapper mapper, 
            ILogger<UpdateHotelCommandHandler> logger, 
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IHotelProvider hotelProvider,
            ICurrentUserService currentUserService)
        {
            _hotel = hotel;
            _country = country;
            _city = city;
            _address = address;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _hotelProvider = hotelProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetHotelDto> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        { 
            var idGuid = Guid.Parse(request.Id);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(h => h.HotelId == idGuid && h.IsActive, cancellationToken);
            if (hotel == null)
            {
                throw new NotFoundException(request);
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
                //если адрес новый, то старый блокируем
                hotel.Address.SetIsActive(false);
                await _hotel.UpdateAsync(hotel, cancellationToken);

                address = new Address(request.Address.Street, request.Address.HouseNumber, request.Address.Latitude, request.Address.Longitude, true, city.CityId);
                await _address.AddAsync(address, cancellationToken);
            }

            hotel.UpdateName(request.Name);
            hotel.UpdateAddres(address!.AddressId);
            hotel.UpdateDescription(request.Description);
            hotel.UpdateIBAN(request.IBAN);
            hotel.UpdateRating(request.Rating);
            hotel.UpdateImage(request.Image);

            var result = await _hotel.UpdateAsync(hotel, cancellationToken);
            await _hotelProvider.UpdateHotelAsync(_currentUserService.Token, request.Id, hotel, cancellationToken);
            _logger.LogInformation($"Hotel {result.HotelId} updated");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return _mapper.Map<GetHotelDto>(result);
        }
    }
}
