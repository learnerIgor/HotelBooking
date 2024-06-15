using MediatR;

namespace HR.Application.Handlers.RoomTypes.Queries.GetRoomType
{
    public class GetRoomTypeQuery : IRequest<GetRoomTypeDto>
    {
        public string Id { get; set; } = default!;
    }
}
