using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Unit>
    {
        private readonly IBaseWriteRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<Address> _address;
        private readonly ILogger<DeleteHotelCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IHotelProvider _hotelProvider;
        private readonly ICurrentUserService _currentUserService;

        public DeleteHotelCommandHandler(
            IBaseWriteRepository<Hotel> hotel, 
            IBaseWriteRepository<Address> address, 
            ILogger<DeleteHotelCommandHandler> logger, 
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IHotelProvider hotelProvider,
            ICurrentUserService currentUserService)
        {
            _hotel = hotel;
            _address = address;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _hotelProvider = hotelProvider;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteHotelCommand request, CancellationToken cancellationToken)
        {
            var hotelId = Guid.Parse(request.Id);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(e => e.HotelId == hotelId, cancellationToken);
            if (hotel is null)
            {
                throw new NotFoundException(request);
            }
            hotel.UpdateIsActive(false);

            var address = await _address.AsAsyncRead().SingleOrDefaultAsync(a => a.AddressId == hotel.AddressId, cancellationToken);
            address!.SetIsActive(false);

            await _hotel.UpdateAsync(hotel, cancellationToken);
            await _hotelProvider.DeleteHotelAsync(_currentUserService.Token, hotelId, cancellationToken);
            _logger.LogWarning($"Hotel {hotel.HotelId} deleted");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return default;
        }
    }
}
