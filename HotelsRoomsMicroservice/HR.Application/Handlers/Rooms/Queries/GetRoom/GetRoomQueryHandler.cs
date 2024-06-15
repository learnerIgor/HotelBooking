using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.Exceptions;
using HR.Domain;
using AutoMapper;
using HR.Application.BaseRealizations;
using HR.Application.Abstractions.Caches.Rooms;

namespace HR.Application.Handlers.Rooms.Queries.GetRoom
{
    public class GetRoomQueryHandler : BaseCashedQuery<GetRoomQuery, GetRoomDto>
    {
        private readonly IBaseReadRepository<Room> _room;
        private readonly IMapper _mapper;

        public GetRoomQueryHandler(IBaseReadRepository<Room> room, IMapper mapper, IRoomMemoryCache roomMemory) : base(roomMemory)
        {
            _room = room;
            _mapper = mapper;
        }

        public override async Task<GetRoomDto> SentQueryAsync(GetRoomQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomId == idGuid && r.IsActive, cancellationToken);
            if (room == null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetRoomDto>(room);
        }
    }
}
