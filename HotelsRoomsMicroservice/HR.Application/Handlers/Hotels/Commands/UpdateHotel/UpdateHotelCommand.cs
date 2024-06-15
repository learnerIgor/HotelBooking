using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommand : CommonCommand, IImage, IAccountCommand, IRequest<GetHotelDto>
    {
        public string Id { get; init; } = default!;
        public string Description { get; set; } = default!;
        public string IBAN { get; init; } = default!;
        public int Rating { get; set; }
        public string Image { get; init; } = default!;
        public AddressDto Address { get; set; } = default!;
    }
}
