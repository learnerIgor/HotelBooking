using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Exceptions;
using HR.Application.Caches;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Unit>
    {
        private readonly IBaseWriteRepository<Room> _room;
        private readonly ILogger<DeleteRoomCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IRoomProvider _roomProvider;
        private readonly ICurrentUserService _currentUserService;

        public DeleteRoomCommandHandler(
            IBaseWriteRepository<Room> room, 
            ILogger<DeleteRoomCommandHandler> logger, 
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IRoomProvider roomProvider,
            ICurrentUserService currentUserService)
        {
            _room = room;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _roomProvider = roomProvider;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var guidId = Guid.Parse(request.Id);
            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(e => e.RoomId == guidId && e.IsActive, cancellationToken);
            if (room == null)
            {
                throw new NotFoundException(request);
            }
            room.UpdateIsActive(false);
            await _room.UpdateAsync(room, cancellationToken);
            await _roomProvider.DeleteRoomAsync(_currentUserService.Token, room.RoomId, cancellationToken);
            _logger.LogWarning($"Room {room.RoomId} deleted");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return default;
        }
    }
}
