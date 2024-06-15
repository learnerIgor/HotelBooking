using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.RoomTypes.Queries.GetRoomTypes
{
    public class GetRoomTypesQuery : ListFilter, IBasePaginationFilter, IRequest<BaseListDto<GetRoomTypeDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
