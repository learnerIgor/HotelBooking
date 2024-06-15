using AutoMapper;
using HR.Application.Abstractions.Caches.Rooms;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Dtos;
using HR.Domain;

namespace HR.Application.Handlers.Rooms.Queries.GetRooms
{
    public class GetRoomsQueryHandler : BaseCashedQuery<GetRoomsQuery, BaseListDto<GetRoomDto>>
    {
        private readonly IBaseReadRepository<Room> _rooms;
        private readonly IMapper _mapper;

        public GetRoomsQueryHandler(IBaseReadRepository<Room> rooms, IMapper mapper, IRoomListMemoryCache roomListMemory) : base(roomListMemory)
        {
            _rooms = rooms;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetRoomDto>> SentQueryAsync(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.HotelId);
            var query = _rooms.AsQueryable().Where(r => r.HotelId == idGuid && r.IsActive);

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }
            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            query = query.OrderBy(e => e.HotelId);

            var rooms = await _rooms.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var countRooms = await _rooms.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetRoomDto[]>(rooms);

            return new BaseListDto<GetRoomDto>
            {
                Items = items,
                TotalCount = countRooms
            };
        }
    }
}
