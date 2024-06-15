namespace HR.ExternalProviders.Models
{
    public class GetCity
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }

        public Guid CountryId { get; set; }
        public GetCountry Country { get; set; } = default!;
    }
}
