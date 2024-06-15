using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.Rooms.UpdateRoom
{
    public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand, GetRoomExternalDto>
    {
        private readonly IBaseReadRepository<Hotel> _hotel;
        private readonly IBaseReadRepository<RoomType> _roomType;
        private readonly IBaseWriteRepository<Room> _room;
        private readonly IBaseWriteRepository<AmenityRoom> _amenityRoom;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateRoomCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateRoomCommandHandler(
            IBaseReadRepository<RoomType> roomType,
            IBaseWriteRepository<Room> room,
            IBaseWriteRepository<AmenityRoom> amenityRoom,
            IBaseReadRepository<Hotel> hotel,
            IMapper mapper,
            ILogger<UpdateRoomCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _roomType = roomType;
            _room = room;
            _mapper = mapper;
            _logger = logger;
            _hotel = hotel;
            _amenityRoom = amenityRoom;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetRoomExternalDto> Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            var idGuidHotel = Guid.Parse(request.HotelId);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(n => n.HotelId == idGuidHotel && n.IsActive, cancellationToken);
            if (hotel == null)
            {
                throw new BadOperationException($"There is no hotel with id {request.HotelId} in AccommoMicroservice.");
            }

            var idRoomTypeHotel = Guid.Parse(request.RoomTypeId);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(n => n.RoomTypeId == idRoomTypeHotel && n.IsActive, cancellationToken);
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
                throw new BadOperationException($"The room already exists in AccommoMicroservice");
            }

            var idGuid = Guid.Parse(request.Id);

            var amenityRoom = await _amenityRoom.AsAsyncRead().ToArrayAsync(r => r.RoomId == idGuid, cancellationToken);
            await _amenityRoom.RemoveRangeAsync(amenityRoom!, cancellationToken);

            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomId == idGuid, cancellationToken);
            if (room == null)
            {
                throw new NotFoundException(request);
            }
            room.UpdateFloor(request.Floor);
            room.UpdateNumber(request.Number);
            room.UpdateRoomType(roomType.RoomTypeId);
            room.UpdateHotel(hotel.HotelId);
            room.UpdateImage(request.Image);
            room.UpdateAmenities(request.Amenities);

            room = await _room.UpdateAsync(room, cancellationToken);
            _logger.LogInformation($"Updated room {room.RoomId} in AccommoMicroservice");
            _cleanAccommoCacheService.ClearAllCaches();

            return _mapper.Map<GetRoomExternalDto>(room);
        }
    }
}
