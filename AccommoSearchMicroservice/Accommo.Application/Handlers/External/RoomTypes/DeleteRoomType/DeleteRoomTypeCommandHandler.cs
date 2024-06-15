using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.RoomTypes.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandler : IRequestHandler<DeleteRoomTypeCommand, Unit>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly ILogger<DeleteRoomTypeCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public DeleteRoomTypeCommandHandler(
            IBaseWriteRepository<RoomType> roomType,
            ILogger<DeleteRoomTypeCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _roomType = roomType;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<Unit> Handle(DeleteRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(e => e.RoomTypeId == idGuid, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            roomType.UpdateIsActive(false);

            await _roomType.UpdateAsync(roomType, cancellationToken);
            _logger.LogWarning($"Type of room {roomType.RoomTypeId} deleted in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearAllCaches();

            return default;
        }
    }
}
