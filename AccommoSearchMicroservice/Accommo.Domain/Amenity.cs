namespace Accommo.Domain
{
    public class Amenity
    {
        public int AmenityId { get; private set; }
        public string Name { get; private set; } = default!;

        public IEnumerable<AmenityRoom> Rooms { get; private set; } = default!;

        private Amenity() { }
    }
}
