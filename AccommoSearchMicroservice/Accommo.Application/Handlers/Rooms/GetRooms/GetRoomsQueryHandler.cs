using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Domain;
using AutoMapper;
using Accommo.Application.BaseRealizations;
using Accommo.Application.Dtos.Rooms;
using Accommo.Application.Dtos;
using Accommo.Application.Exceptions;
using Accommo.Application.Abstractions.Caches.Rooms;

namespace Accommo.Application.Handlers.Rooms.GetRooms
{
    public class GetRoomsQueryHandler : BaseCashedQuery<GetRoomsQuery, BaseListDto<GetRoomDto>>
    {
        private readonly IBaseReadRepository<Room> _rooms;
        private readonly IMapper _mapper;

        public GetRoomsQueryHandler(IBaseReadRepository<Room> rooms, IMapper mapper, IRoomListMemoryCache listMemoryCache) : base(listMemoryCache)
        {
            _rooms = rooms;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetRoomDto>> SentQueryAsync(GetRoomsQuery request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Parse(request.StartDate);
            var endDate = DateTime.Parse(request.EndDate);
            if (startDate < DateTime.Now.Date || endDate < DateTime.Now.Date)
            {
                throw new BadOperationException("Incorrect dates");
            }

            var idGuid = Guid.Parse(request.HotelId);
            var query = _rooms.AsQueryable().Where(room => room.Reservations.All(r => (r.CheckInDate > startDate && r.CheckInDate > endDate)
                                                                                  || (r.CheckOutDate < startDate && r.CheckOutDate < endDate))
                                                        && room.HotelId == idGuid && room.IsActive);

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }
            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            query = query.OrderBy(e => e.RoomId);

            var entitiesResult = await _rooms.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _rooms.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetRoomDto[]>(entitiesResult);

            return new BaseListDto<GetRoomDto>
            { 
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
