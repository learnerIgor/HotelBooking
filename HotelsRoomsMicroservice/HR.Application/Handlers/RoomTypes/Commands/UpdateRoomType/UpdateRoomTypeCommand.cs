using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.RoomTypes.Commands.UpdateRoomType
{
    public class UpdateRoomTypeCommand : CommonCommand, IRequest<GetRoomTypeDto>
    {
        public string Id { get; init; } = default!;
    }
}
