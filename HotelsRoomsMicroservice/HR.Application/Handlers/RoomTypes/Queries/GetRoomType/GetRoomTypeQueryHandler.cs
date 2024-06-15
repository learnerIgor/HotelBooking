using AutoMapper;
using HR.Application.Abstractions.Caches.RoomTypes;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Exceptions;
using HR.Domain;

namespace HR.Application.Handlers.RoomTypes.Queries.GetRoomType
{
    public class GetRoomTypeQueryHandler : BaseCashedQuery<GetRoomTypeQuery, GetRoomTypeDto>
    {
        private readonly IBaseReadRepository<RoomType> _roomType;
        private readonly IMapper _mapper;

        public GetRoomTypeQueryHandler(IBaseReadRepository<RoomType> roomType, IMapper mapper, IRoomTypeMemoryCache memoryCache) : base(memoryCache)
        {
            _roomType = roomType;
            _mapper = mapper;
        }

        public override async Task<GetRoomTypeDto> SentQueryAsync(GetRoomTypeQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var roomType = await _roomType.AsAsyncRead().SingleOrDefaultAsync(i => i.RoomTypeId == idGuid && i.IsActive, cancellationToken);
            if (roomType == null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetRoomTypeDto>(roomType);
        }
    }
}
