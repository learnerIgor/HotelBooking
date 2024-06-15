using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Exceptions;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.Rooms.DeleteRoom
{
    public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand, Unit>
    {
        private readonly IBaseWriteRepository<Room> _room;
        private readonly ILogger<DeleteRoomCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public DeleteRoomCommandHandler(
            IBaseWriteRepository<Room> room,
            ILogger<DeleteRoomCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _room = room;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<Unit> Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            var guidId = Guid.Parse(request.Id);
            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(e => e.RoomId == guidId, cancellationToken);
            if (room == null)
            {
                throw new NotFoundException(request);
            }
            room.UpdateIsActive(false);
            await _room.UpdateAsync(room, cancellationToken);
            _logger.LogWarning($"Room {room.RoomId} deleted in AccommoMicroservice");
            _cleanAccommoCacheService.ClearAllCaches();

            return default;
        }
    }
}
