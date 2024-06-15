namespace Accommo.Application.Handlers.External.Hotels.UpdateHotel
{
    public class UpdateHotelPayload
    {
        public Guid AddressId { get; set; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int Rating { get; init; }
        public string Image { get; init; } = default!;
        public GetAddressExternalDto Address { get; init; } = default!;
    }
}