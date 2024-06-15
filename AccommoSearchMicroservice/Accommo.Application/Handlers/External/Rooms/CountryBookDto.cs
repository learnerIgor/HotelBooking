namespace Accommo.Application.Handlers.External.Rooms
{
    public class CountryBookDto
    {
        public Guid CountryId { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
