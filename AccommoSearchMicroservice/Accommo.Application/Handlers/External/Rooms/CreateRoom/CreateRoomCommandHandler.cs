using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Exceptions;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.Rooms.CreateRoom
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, GetRoomExternalDto>
    {
        private readonly IBaseReadRepository<Hotel> _hotel;
        private readonly IBaseWriteRepository<RoomType> _roomType;
        private readonly IBaseWriteRepository<Room> _room;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateRoomCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public CreateRoomCommandHandler(
            IBaseReadRepository<Hotel> hotel,
            IBaseWriteRepository<RoomType> roomType,
            IBaseWriteRepository<Room> room,
            IMapper mapper,
            ILogger<CreateRoomCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _hotel = hotel;
            _roomType = roomType;
            _room = room;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }
        public async Task<GetRoomExternalDto> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var idHotelGuid = Guid.Parse(request.HotelId);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(n => n.HotelId == idHotelGuid && n.IsActive, cancellationToken);
            if (hotel == null)
            {
                throw new BadOperationException($"There is no hotel called {request.HotelId} in AccommoMicroservice.");
            }

            var idRoomTypeGuid = Guid.Parse(request.RoomTypeId);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(n => n.RoomTypeId == idRoomTypeGuid && n.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new BadOperationException($"There is no type of number called {request.RoomTypeId} in AccommoMicroservice.");
            }

            var isRoomExist = await _room.AsAsyncRead().AnyAsync(e => e.Number == request.Number
                                                                                && e.Floor == request.Floor
                                                                                && e.RoomTypeId == roomType.RoomTypeId
                                                                                && e.HotelId == hotel.HotelId
                                                                                && e.IsActive
                                                                                , cancellationToken);

            if (isRoomExist)
            {
                throw new BadOperationException($"The room already exists in AccommoMicroservice.");
            }

            var room = new Room(request.RoomId, request.Floor, request.Number, roomType.RoomTypeId, true, hotel.HotelId, request.Amenities, request.Image);
            room = await _room.AddAsync(room, cancellationToken);
            _logger.LogInformation($"New room {room.RoomId} created in AccommoMicroservice.");
            _cleanAccommoCacheService.ClearListCaches();

            return _mapper.Map<GetRoomExternalDto>(room);
        }
    }
}
