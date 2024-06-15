namespace Accommo.Application.Handlers.External.Hotels
{
    public class GetCountryExternalDto
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = default!;
    }
}
