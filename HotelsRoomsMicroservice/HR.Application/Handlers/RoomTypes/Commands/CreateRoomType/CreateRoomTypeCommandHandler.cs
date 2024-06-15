using AutoMapper;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Application.Caches;
using HR.Application.Exceptions;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommandHandler : IRequestHandler<CreateRoomTypeCommand, GetRoomTypeDto>
    {
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomTypeCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IRoomTypeProvider _roomTypeProvider;
        private readonly ICurrentUserService _currentUserService;
        public CreateRoomTypeCommandHandler(
            IBaseWriteRepository<RoomType> roomType,
            IMapper mapper,
            ILogger<CreateRoomTypeCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IRoomTypeProvider roomTypeProvider,
            ICurrentUserService currentUserService)
        {
            _roomType = roomType;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _roomTypeProvider = roomTypeProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetRoomTypeDto> Handle(CreateRoomTypeCommand request, CancellationToken cancellationToken)
        {
            var isExist = await _roomType.AsAsyncRead().SingleOrDefaultAsync(r => r.Name == request.Name && r.IsActive, cancellationToken);
            if (isExist != null)
            {
                throw new BadOperationException($"Type of room with name {request.Name} already exists.");
            }
            var roomType = new RoomType(request.Name, request.BaseCost, true);

            roomType = await _roomType.AddAsync(roomType, cancellationToken);
            await _roomTypeProvider.AddRoomTypeAsync(_currentUserService.Token, roomType, cancellationToken);
            _logger.LogInformation($"New type of room {roomType.RoomTypeId} created.");
            _cleanHotelRoomCacheService.ClearListCaches();

            return _mapper.Map<GetRoomTypeDto>(roomType);
        }
    }
}
