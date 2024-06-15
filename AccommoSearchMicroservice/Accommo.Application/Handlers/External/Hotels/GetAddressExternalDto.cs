namespace Accommo.Application.Handlers.External.Hotels
{
    public class GetAddressExternalDto
    {
        public string Street { get; set; } = default!;
        public string HouseNumber { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public GetCityExternalDto City { get; set; } = default!;
    }
}
