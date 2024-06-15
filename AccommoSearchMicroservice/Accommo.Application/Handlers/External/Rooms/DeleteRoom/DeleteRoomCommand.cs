using MediatR;

namespace Accommo.Application.Handlers.External.Rooms.DeleteRoom
{
    public class DeleteRoomCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
