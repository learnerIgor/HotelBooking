namespace HR.ExternalProviders.Models
{
    public class GetRoom
    {
        public Guid RoomId { get; set; }
        public int Floor { get; set; }
        public int Number { get; set; }
        public bool IsActive { get; set; }
        public string Image { get; set; } = default!;
        public Guid RoomTypeId { get; set; }
        public Guid HotelId { get; set; }
        public int[] Amenities { get; set; } = default!;
    }
}
