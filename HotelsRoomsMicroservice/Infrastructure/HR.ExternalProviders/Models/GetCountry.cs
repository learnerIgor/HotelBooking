namespace HR.ExternalProviders.Models
{
    public class GetCountry
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
