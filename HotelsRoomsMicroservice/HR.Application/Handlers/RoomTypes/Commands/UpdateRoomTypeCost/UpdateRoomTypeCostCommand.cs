using MediatR;

namespace HR.Application.Handlers.RoomTypes.Commands.UpdateRoomTypeCost
{
    public class UpdateRoomTypeCostCommand : IRequest<GetRoomTypeDto>
    {
        public string Id { get; set; } = default!;
        public decimal BaseCost { get; set; }
    }
}
