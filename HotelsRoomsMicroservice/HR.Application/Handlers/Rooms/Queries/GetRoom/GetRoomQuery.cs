using MediatR;

namespace HR.Application.Handlers.Rooms.Queries.GetRoom
{
    public class GetRoomQuery : IRequest<GetRoomDto>
    {
        public string Id { get; set; } = default!;
    }
}
