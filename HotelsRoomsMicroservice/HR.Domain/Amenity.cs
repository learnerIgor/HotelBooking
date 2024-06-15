namespace HR.Domain
{
    public class Amenity
    {
        public int AmenityId { get; private set; }
        public string Name { get; private set; }

        public IEnumerable<AmenityRoom> Rooms { get; private set; }

        private Amenity() { }
    }
}
