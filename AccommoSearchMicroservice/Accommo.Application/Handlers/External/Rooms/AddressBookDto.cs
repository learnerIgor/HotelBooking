namespace Accommo.Application.Handlers.External.Rooms
{
    public class AddressBookDto
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; } = default!;
        public string HouseNumber { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsActive {  get; set; }
        public CityBookDto City { get; set; } = default!;

    }
}
