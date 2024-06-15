namespace Accommo.Domain
{
    public class Amenity
    {
        public int AmenityId { get; set; }
        public string Name { get; set; } = default!;

        public IEnumerable<AmenityRoom> Rooms { get; set; } = default!;

        private Amenity() { }
    }
}
