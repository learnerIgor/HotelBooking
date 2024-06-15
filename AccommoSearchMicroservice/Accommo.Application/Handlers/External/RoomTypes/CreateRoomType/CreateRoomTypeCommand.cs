using MediatR;

namespace Accommo.Application.Handlers.External.RoomTypes.CreateRoomType
{
    public class CreateRoomTypeCommand : IRequest<GetRoomTypeExternalDto>
    {
        public Guid RoomTypeId { get; set; }
        public string Name { get; set; } = default!;
        public decimal BaseCost { get; set; }
        public bool IsActive { get; set; }
    }
}
