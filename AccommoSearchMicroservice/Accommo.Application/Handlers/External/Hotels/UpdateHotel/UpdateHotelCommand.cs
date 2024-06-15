using MediatR;

namespace Accommo.Application.Handlers.External.Hotels.UpdateHotel
{
    public class UpdateHotelCommand : IRequest<GetHotelExternalDto>
    {
        public string Id { get; init; } = default!;
        public string Name { get; init; } = default!;
        public string Description { get; init; } = default!;
        public int Rating { get; init; }
        public string Image { get; init; } = default!;

        public Guid AddressId { get; init; }
        public GetAddressExternalDto Address { get; init; } = default!;
    }
}
