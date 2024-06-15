using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using HR.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using HR.Application.Exceptions;
using HR.Application.Utils;
using HR.Application.Caches;
using HR.Application.Abstractions.ExternalProviders;
using HR.Application.Abstractions.Service;

namespace HR.Application.Handlers.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, GetRoomDto>
    {
        private readonly IBaseReadRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IBaseWriteRepository<Room> _room;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomCommandHandler> _logger;
        private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
        private readonly IRoomProvider _roomProvider;
        private readonly ICurrentUserService _currentUserService;

        public CreateRoomCommandHandler(
            IBaseReadRepository<Hotel> hotel,
            IBaseWriteRepository<RoomType> roomType,
            IBaseWriteRepository<Room> room,
            IMapper mapper,
            ILogger<CreateRoomCommandHandler> logger,
            ICleanHotelRoomCacheService cleanHotelRoomCacheService,
            IRoomProvider roomProvider,
            ICurrentUserService currentUserService)
        {
            _hotel = hotel;
            _roomType = roomType;
            _room = room;
            _mapper = mapper;
            _logger = logger;
            _cleanHotelRoomCacheService = cleanHotelRoomCacheService;
            _roomProvider = roomProvider;
            _currentUserService = currentUserService;
        }
        public async Task<GetRoomDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var idHotelGuid = Guid.Parse(request.HotelId);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(n => n.HotelId == idHotelGuid && n.IsActive, cancellationToken);
            if (hotel == null)
            {
                throw new BadOperationException($"There is no hotel called {request.HotelId}.");
            }

            var idRoomTypeGuid = Guid.Parse(request.RoomTypeId);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(n => n.RoomTypeId == idRoomTypeGuid && n.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new BadOperationException($"There is no type of number called {request.RoomTypeId}.");
            }

            var isRoomExist = await _room.AsAsyncRead().AnyAsync(e => e.Number == request.Number
                                                                                && e.Floor == request.Floor
                                                                                && e.RoomTypeId == roomType.RoomTypeId
                                                                                && e.HotelId == hotel.HotelId
                                                                                && e.IsActive
                                                                                , cancellationToken);

            if (isRoomExist)
            {
                throw new BadOperationException($"The room already exists");
            }

            var room = new Room(request.Floor, request.Number, roomType.RoomTypeId, true, hotel.HotelId, AmenityRoomUtil.GetAmenitiesRoom(request.Amenities).ToArray(), request.Image);
            room = await _room.AddAsync(room, cancellationToken);
            await _roomProvider.AddRoomAsync(_currentUserService.Token, room, cancellationToken);
            _logger.LogInformation($"New room {room.RoomId} created.");
            _cleanHotelRoomCacheService.ClearListCaches();

            return _mapper.Map<GetRoomDto>(room);
        }
    }
}
