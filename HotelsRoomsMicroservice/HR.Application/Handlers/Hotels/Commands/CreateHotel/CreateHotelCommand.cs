using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommand : CommonCommand, IAccountCommand, IImage, IRequest<GetHotelDto>
    {
        public string Description { get; init; } = default!;
        public string IBAN { get; init; } = default!;
        public int Rating { get; init; }
        public string Image { get; init; } = default!;
        public AddressDto Address { get; init; } = default!;

    }
}
