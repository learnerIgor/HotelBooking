namespace HR.ExternalProviders.Models
{
    public class GetAddress
    {
        public Guid AddressId { get; set; }
        public string Street { get; set; } = default!;
        public string HouseNumber { get; set; } = default!;
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public bool IsActive { get; set; }

        public int CityId { get; set; }
        public GetCity City { get; set; } = default!;
    }
}