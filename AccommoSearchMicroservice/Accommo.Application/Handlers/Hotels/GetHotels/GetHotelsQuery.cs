using Accommo.Application.Dtos;
using Accommo.Application.Dtos.Hotels;
using MediatR;

namespace Accommo.Application.Handlers.Hotels.GetHotels
{
    public class GetHotelsQuery : IBasePaginationFilter, IRequest<BaseListDto<GetHotelDto>>
    {
        public string LocationText { get; init; } = default!;
        public string StartDate { get; init; } = default!;
        public string EndDate { get; init; } = default!;
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
