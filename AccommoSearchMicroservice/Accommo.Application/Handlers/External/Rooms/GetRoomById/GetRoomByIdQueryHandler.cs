using Accommo.Application.Abstractions;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.BaseRealizations;
using Accommo.Domain;
using AutoMapper;
using Accommo.Application.Exceptions;

namespace Accommo.Application.Handlers.External.Rooms.GetRoomById
{
    public class GetRoomByIdQueryHandler : BaseCashedQuery<GetRoomByIdQuery, GetRoomBookDto>
    {
        private readonly IBaseReadRepository<Room> _room;
        private readonly IMapper _mapper;
        public GetRoomByIdQueryHandler(IBaseReadRepository<Room> room, IMapper mapper, IRoomBookMemoryCache memoryCache) : base(memoryCache)
        {
            _room = room;
            _mapper = mapper;
        }

        public override async Task<GetRoomBookDto> SentQueryAsync(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomId == idGuid && r.IsActive, cancellationToken);
            if (room == null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetRoomBookDto>(room);
        }
    }
}
