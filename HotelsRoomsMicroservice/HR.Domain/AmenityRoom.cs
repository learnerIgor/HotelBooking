namespace HR.Domain
{
    public class AmenityRoom
    {
        public Guid RoomId { get; private set; }
        public Room Room { get; private set; }

        public int AmenityId { get; private set; }
        public Amenity Amenity { get; private set; }

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
