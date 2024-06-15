using HR.Application.Dtos;
using MediatR;

namespace HR.Application.Handlers.Hotels.Queries.GetHotels
{
    public class GetHotelsQuery : ListFilter, IBasePaginationFilter, IRequest<BaseListDto<GetHotelDto>>
    {
        public int? Limit { get; init; }
        public int? Offset { get; init; }
    }
}
