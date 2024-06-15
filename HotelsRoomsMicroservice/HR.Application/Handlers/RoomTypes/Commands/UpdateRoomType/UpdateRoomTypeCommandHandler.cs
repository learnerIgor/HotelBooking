using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Exceptions;
using HR.Application.Caches;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.RoomTypes.Commands.UpdateRoomType
{
    public class UpdateRoomTypeCommandHandler : IRequestHandler<UpdateRoomTypeCommand, GetRoomTypeDto>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomTypeCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IRoomTypeProvider _roomTypeProvider;
        private readonly ICurrentUserService _currentUserService;

        public UpdateRoomTypeCommandHandler(
            IBaseWriteRepository<RoomType> roomType, 
            IMapper mapper, 
            ILogger<UpdateRoomTypeCommandHandler> logger, 
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IRoomTypeProvider roomTypeProvider,
            ICurrentUserService currentUserService)
        {
            _roomType = roomType;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _roomTypeProvider = roomTypeProvider;
        }
        public async Task<GetRoomTypeDto> Handle(UpdateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(i => i.RoomTypeId == idGuid && i.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            roomType.UpdateName(request.Name);

            var result = await _roomType.UpdateAsync(roomType, cancellationToken);
            await _roomTypeProvider.UpdateRoomTypeAsync(_currentUserService.Token, request.Id, result, cancellationToken);
            _logger.LogInformation($"Type of room {result.RoomTypeId} updated");
            _cleanHotelRoomCacheService.ClearAllCaches();

            return _mapper.Map<GetRoomTypeDto>(result);
        }
    }
}
