using MediatR;

namespace Accommo.Application.Handlers.External.Hotels.CreateHotel
{
    public class CreateHotelCommand : IRequest<GetHotelExternalDto>
    {
        public Guid HotelId { get; set; }
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int Rating { get; init; }
        public string Image { get; init; } = default!;

        public Guid AddressId { get; init; }
        public GetAddressExternalDto Address { get; init; } = default!;

    }
}
