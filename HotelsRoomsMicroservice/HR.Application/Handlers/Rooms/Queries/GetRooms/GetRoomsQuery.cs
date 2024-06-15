using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Rooms.Queries.GetRooms
{
    public class GetRoomsQuery : IBasePaginationFilter, IRequest<BaseListDto<GetRoomDto>>
    {
        public string HotelId { get; set; } = default!;
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
