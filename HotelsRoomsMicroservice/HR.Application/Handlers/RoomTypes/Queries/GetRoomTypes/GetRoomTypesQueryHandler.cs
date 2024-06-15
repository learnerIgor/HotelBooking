using AutoMapper;
using HR.Application.Abstractions.Caches.RoomTypes;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Dtos;
using HR.Domain;

namespace HR.Application.Handlers.RoomTypes.Queries.GetRoomTypes
{
    public class GetRoomTypesQueryHandler : BaseCashedQuery<GetRoomTypesQuery, BaseListDto<GetRoomTypeDto>>
    {
        private readonly IBaseReadRepository<RoomType> _roomTypes;
        private readonly IMapper _mapper;

        public GetRoomTypesQueryHandler(IBaseReadRepository<RoomType> roomTypes, IMapper mapper, IRoomTypeListMemoryCache roomTypeListMemory) : base(roomTypeListMemory)
        {
            _roomTypes = roomTypes;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetRoomTypeDto>> SentQueryAsync(GetRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var query = _roomTypes.AsQueryable().Where(ListWhere.WhereRoomTypes(request));

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }
            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            query = query.OrderBy(e => e.RoomTypeId);

            var entityResult = await _roomTypes.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entityCount = await _roomTypes.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetRoomTypeDto[]>(entityResult);
            return new BaseListDto<GetRoomTypeDto>
            {
                Items = items,
                TotalCount = entityCount
            };
        }
    }
}
