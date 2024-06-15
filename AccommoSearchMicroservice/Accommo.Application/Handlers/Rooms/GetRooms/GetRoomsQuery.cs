using Accommo.Application.Dtos;
using Accommo.Application.Dtos.Rooms;
using MediatR;

namespace Accommo.Application.Handlers.Rooms.GetRooms
{
    public class GetRoomsQuery : IBasePaginationFilter, IRequest<BaseListDto<GetRoomDto>>
    {
        public string HotelId { get; init; } = default!;
        public string StartDate { get; init; } = default!;
        public string EndDate { get; init; } = default!;
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
