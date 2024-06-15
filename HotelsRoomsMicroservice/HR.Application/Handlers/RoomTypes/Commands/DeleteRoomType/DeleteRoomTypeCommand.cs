using MediatR;

namespace HR.Application.Handlers.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
