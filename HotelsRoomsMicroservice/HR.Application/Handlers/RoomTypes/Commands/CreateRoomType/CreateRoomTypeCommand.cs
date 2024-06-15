using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommand : CommonCommand, IRequest<GetRoomTypeDto>
    {
        public decimal BaseCost { get; set; }
    }
}
