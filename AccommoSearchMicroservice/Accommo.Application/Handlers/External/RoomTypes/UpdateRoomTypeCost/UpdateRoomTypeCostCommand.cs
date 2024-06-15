using MediatR;

namespace Accommo.Application.Handlers.External.RoomTypes.UpdateRoomTypeCost
{
    public class UpdateRoomTypeCostCommand : IRequest<GetRoomTypeExternalDto>
    {
        public string Id { get; set; } = default!;
        public decimal BaseCost { get; set; }
    }
}
