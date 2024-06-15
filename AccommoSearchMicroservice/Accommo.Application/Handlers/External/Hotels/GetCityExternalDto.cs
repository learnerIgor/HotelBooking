namespace Accommo.Application.Handlers.External.Hotels
{
    public class GetCityExternalDto
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = default!;
        public GetCountryExternalDto Country { get; set; } = default!;
    }
}
