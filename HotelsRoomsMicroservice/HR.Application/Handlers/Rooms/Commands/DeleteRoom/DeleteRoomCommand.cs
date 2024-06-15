using MediatR;

namespace HR.Application.Handlers.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
