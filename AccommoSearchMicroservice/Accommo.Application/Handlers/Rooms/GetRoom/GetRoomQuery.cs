using Accommo.Application.Dtos.Rooms;
using MediatR;

namespace Accommo.Application.Handlers.Rooms.GetRoom
{
    public class GetRoomQuery : IRequest<GetRoomDto>
    {
        public string Id { get; set; } = default!;
    }
}
