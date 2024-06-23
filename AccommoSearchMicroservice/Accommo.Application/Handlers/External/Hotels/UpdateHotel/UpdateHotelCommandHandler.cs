using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Exceptions;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.Hotels.UpdateHotel
{
    public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand, GetHotelExternalDto>
    {
        private readonly IBaseWriteRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<Address> _address;
        private readonly IBaseWriteRepository<City> _city;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateHotelCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateHotelCommandHandler(
            IBaseWriteRepository<Hotel> hotel,
            IBaseWriteRepository<Address> address,
            IBaseWriteRepository<City> city,
            IMapper mapper,
            ILogger<UpdateHotelCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _hotel = hotel;
            _address = address;
            _city = city;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetHotelExternalDto> Handle(UpdateHotelCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(h => h.HotelId == idGuid && h.IsActive, cancellationToken);
            if (hotel == null)
            {
                throw new NotFoundException(request);
            }

            var city = await _city.AsAsyncRead().SingleOrDefaultAsync(h => h.CityId == request.Address.City.CityId && h.IsActive, cancellationToken);

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

                address = new Address(request.AddressId, request.Address.Street, request.Address.HouseNumber, request.Address.Latitude, request.Address.Longitude, true, city!.CityId);
                await _address.AddAsync(address, cancellationToken);
            }

            hotel.UpdateName(request.Name);
            hotel.UpdateAddres(address.AddressId);
            hotel.UpdateDescription(request.Description);
            hotel.UpdateRating(request.Rating);
            hotel.UpdateImage(request.Image);

            var result = await _hotel.UpdateAsync(hotel, cancellationToken);
            _logger.LogInformation($"Hotel {request.Id} updated");
            _cleanAccommoCacheService.ClearAllCaches();

            return _mapper.Map<GetHotelExternalDto>(result);
        }
    }
}
