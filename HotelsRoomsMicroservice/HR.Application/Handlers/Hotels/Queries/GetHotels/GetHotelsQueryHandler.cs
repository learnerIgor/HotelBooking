using AutoMapper;
using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Caches;
using HR.Application.Dtos;
using HR.Domain;

namespace HR.Application.Handlers.Hotels.Queries.GetHotels
{
    public class GetHotelsQueryHandler : BaseCashedQuery<GetHotelsQuery, BaseListDto<GetHotelDto>>
    {
        private readonly IBaseReadRepository<Hotel> _hotels;
        private readonly IMapper _mapper;

        public GetHotelsQueryHandler
            (IBaseReadRepository<Hotel> hotels, 
            IMapper mapper, 
            IHotelListMemoryCache listMemoryCache) : base(listMemoryCache)
        {
            _hotels = hotels;
            _mapper = mapper;
        }

        public override async Task<BaseListDto<GetHotelDto>> SentQueryAsync(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            var query = _hotels.AsQueryable().Where(ListWhere.WhereHotels(request));

            if (request.Offset.HasValue)
            {
                query = query.Skip(request.Offset.Value);
            }
            if (request.Limit.HasValue)
            {
                query = query.Take(request.Limit.Value);
            }

            query = query.OrderBy(e => e.HotelId);

            var entitiesResult = await _hotels.AsAsyncRead().ToArrayAsync(query, cancellationToken);
            var entitiesCount = await _hotels.AsAsyncRead().CountAsync(query, cancellationToken);

            var items = _mapper.Map<GetHotelDto[]>(entitiesResult);
            return new BaseListDto<GetHotelDto>
            {
                Items = items,
                TotalCount = entitiesCount
            };
        }
    }
}
