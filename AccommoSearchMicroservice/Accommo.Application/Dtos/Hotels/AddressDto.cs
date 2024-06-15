namespace Accommo.Application.Dtos.Hotels
{
    public class AddressDto
    {
        public string Street { get; set; } = default!;
        public string HouseNumber { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public CityDto City { get; set; } = default!;
    }
}
