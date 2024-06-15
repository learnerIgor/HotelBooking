namespace HR.Application.Handlers.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelPayload
    {
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public string IBAN { get; init; } = default!;
        public int Rating { get; init; }
        public string Image { get; init; } = default!;
        public AddressDto Address { get; init; } = default!;
    }
}