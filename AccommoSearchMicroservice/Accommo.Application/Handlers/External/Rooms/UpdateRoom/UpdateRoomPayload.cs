namespace Accommo.Application.Handlers.External.Rooms.UpdateRoom
{
    public class UpdateRoomPayload
    {
        public int Floor { get; init; }
        public int Number { get; init; }
        public string RoomTypeId { get; init; } = default!;
        public string HotelId { get; init; } = default!;
        public string Image { get; init; } = default!;
        public int[] Amenities { get; set; } = default!;
    }
}
