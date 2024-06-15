using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Unit>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly ILogger<DeleteRoomTypeCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IRoomTypeProvider _roomTypeProvider;
        private readonly ICurrentUserService _currentUserService;

        public DeleteRoomTypeCommandHandler(
            IBaseWriteRepository<RoomType> roomType, 
            ILogger<DeleteRoomTypeCommandHandler> logger, 
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IRoomTypeProvider roomTypeProvider,
            ICurrentUserService currentUserService)
        {
            _roomType = roomType;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _roomTypeProvider = roomTypeProvider;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(e => e.RoomTypeId == idGuid && e.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            roomType.UpdateIsActive(false);

            await _roomType.UpdateAsync(roomType, cancellationToken);
            await _roomTypeProvider.DeleteRoomTypeAsync(_currentUserService.Token, idGuid, cancellationToken);
            _logger.LogWarning($"Type of room {roomType.RoomTypeId} deleted");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return default;
        }
    }
}
