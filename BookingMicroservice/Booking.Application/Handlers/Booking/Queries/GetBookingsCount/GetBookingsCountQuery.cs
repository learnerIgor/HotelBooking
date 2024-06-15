using MediatR;

namespace Booking.Application.Handlers.Booking.Queries.GetBookingsCount
{
    public class GetBookingsCountQuery : IRequest<int>
    {
        public string StartDate { get; init; } = default!;
        public string EndDate { get; init; } = default!;
    }
}