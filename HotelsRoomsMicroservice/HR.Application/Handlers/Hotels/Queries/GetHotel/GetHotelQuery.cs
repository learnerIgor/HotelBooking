using MediatR;

namespace HR.Application.Handlers.Hotels.Queries.GetHotel
{
    public class GetHotelQuery : IRequest<GetHotelDto>
    {
        public string Id { get; init; } = default!;
    }
}
