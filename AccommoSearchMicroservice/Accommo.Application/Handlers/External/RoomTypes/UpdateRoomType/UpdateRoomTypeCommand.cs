using MediatR;

namespace Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType
{
    public class UpdateRoomTypeCommand : IRequest<GetRoomTypeExternalDto>
    {
        public string Id { get; init; } = default!;
        public string Name { get; set; } = default!;
    }
}
