namespace HR.Application.Handlers.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomPayload
    {
        public int Floor { get; init; }
        public int Number { get; init; }
        public string RoomTypeId { get; init; } = default!;
        public string HotelId { get; init; } = default!;
        public string Image { get; init; } = default!;

        public Amenities Amenities { get; init; } = default!;
    }
}
