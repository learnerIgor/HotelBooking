using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.Hotels.DeleteHotel
{
    public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand, Unit>
    {
        private readonly IBaseWriteRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<Address> _address;
        private readonly ILogger<DeleteHotelCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        public DeleteHotelCommandHandler(
            IBaseWriteRepository<Hotel> hotel,
            IBaseWriteRepository<Address> address,
            ILogger<DeleteHotelCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _hotel = hotel;
            _address = address;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
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
            _logger.LogWarning($"Hotel {hotel.HotelId} deleted");
            _cleanAccommoCacheService.ClearAllCaches();

            return default;
        }
    }
}
