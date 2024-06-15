using Accommo.Domain;
namespace Accommo.Application.Handlers.External.Rooms
{
    public class CityBookDto
    {
        public Guid CityId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
        public CountryBookDto Country { get; set; } = default!;
    }
}
