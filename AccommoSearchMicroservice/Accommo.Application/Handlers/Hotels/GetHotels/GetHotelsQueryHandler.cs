using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.BaseRealizations;
using Accommo.Domain;
using Accommo.Application.Dtos.Hotels;
using Accommo.Application.Dtos;
using Accommo.Application.Exceptions;
using Accommo.Application.Abstractions.Caches.Hotels;

namespace Accommo.Application.Handlers.Hotels.GetHotels
{
    internal class GetHotelsQueryHandler : BaseCashedQuery<GetHotelsQuery, BaseListDto<GetHotelDto>>
    {
        private readonly IBaseReadRepository<Hotel> _hotels;
        private readonly IBaseReadRepository<Room> _rooms;
        private readonly IMapper _mapper;

        public GetHotelsQueryHandler(
            IBaseReadRepository<Hotel> hotels, 
            IBaseReadRepository<Room> rooms, 
            IMapper mapper, 
            IHotelListMemoryCache listMemoryCache) : base(listMemoryCache)
        {
            _hotels = hotels;
            _mapper = mapper;
            _rooms = rooms;
        }

        public override async Task<BaseListDto<GetHotelDto>> SentQueryAsync(GetHotelsQuery request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Parse(request.StartDate);
            var endDate = DateTime.Parse(request.EndDate);
            if (startDate < DateTime.Now.Date || endDate < DateTime.Now.Date)
            {
                throw new BadOperationException("Incorrect dates");
            }

            var hotelIds = _rooms.AsQueryable()
                .Where(room => room.Reservations.Where(r => r.IsActive).All(r => (r.CheckInDate > startDate && r.CheckInDate > endDate) 
                    || (r.CheckOutDate < startDate && r.CheckOutDate < endDate)))
                .Select(room => room.HotelId)
                .Distinct();

            var query = _hotels.AsQueryable()
                .Where(h => h.IsActive && (h.Address.City.Country.Name.Contains(request.LocationText)
                    || h.Address.City.Name.Contains(request.LocationText)
                    || h.Address.Street.Contains(request.LocationText)))
                .Where(h => hotelIds.Contains(h.HotelId));

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
