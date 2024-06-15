using Accommo.Application.Dtos.Hotels;
using MediatR;

namespace Accommo.Application.Handlers.Hotels.GetHotel
{
    public class GetHotelQuery : IRequest<GetHotelDto>
    {
        public string Id { get; init; } = default!;
    }
}
