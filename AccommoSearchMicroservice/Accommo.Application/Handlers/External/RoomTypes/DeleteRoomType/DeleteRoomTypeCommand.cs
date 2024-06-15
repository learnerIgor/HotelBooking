using MediatR;

namespace Accommo.Application.Handlers.External.RoomTypes.DeleteRoomType
{
    public class DeleteRoomTypeCommand : IRequest<Unit>
    {
        public string Id { get; set; } = default!;
    }
}
