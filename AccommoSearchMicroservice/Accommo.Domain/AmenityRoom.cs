namespace Accommo.Domain
{
    public class AmenityRoom
    {
        public Guid RoomId { get; set; }
        public Room Room { get; set; } = default!;

        public int AmenityId { get; set; }
        public Amenity Amenity { get; set; } = default!;

        private AmenityRoom() { }

        public AmenityRoom(int amenityId)
        {
            if (amenityId <= 0)
            {
                throw new ArgumentException("Amenity must be more than 0", nameof(amenityId));
            }
            AmenityId = amenityId;
        }
    }
}
