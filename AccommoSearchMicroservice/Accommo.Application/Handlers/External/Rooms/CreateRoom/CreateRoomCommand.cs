using MediatR;

namespace Accommo.Application.Handlers.External.Rooms.CreateRoom
{
    public class CreateRoomCommand : IRequest<GetRoomExternalDto>
    {
        public Guid RoomId { get; set; } = default!;
        public int Floor { get; init; }
        public int Number { get; init; }
        public string RoomTypeId { get; init; } = default!;
        public string HotelId { get; init; } = default!;
        public string Image { get; init; } = default!;

        public int[] Amenities { get; set; } = default!;
    }
}
