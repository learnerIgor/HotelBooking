using AutoMapper;
using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.BaseRealizations;
using HR.Application.Exceptions;
using HR.Domain;

namespace HR.Application.Handlers.Hotels.Queries.GetHotel
{
    public class GetHotelQueryHandler : BaseCashedQuery<GetHotelQuery, GetHotelDto>
    {
        private readonly IBaseReadRepository<Hotel> _hotel;
        private readonly IMapper _mapper;

        public GetHotelQueryHandler(IBaseReadRepository<Hotel> hotel, IMapper mapper, IHotelMemoryCache memoryCache) : base(memoryCache)
        {
            _hotel = hotel;
            _mapper = mapper;
        }

        public override async Task<GetHotelDto> SentQueryAsync(GetHotelQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var hotel = await _hotel.AsAsyncRead().SingleOrDefaultAsync(i => i.HotelId == idGuid && i.IsActive, cancellationToken);
            if (hotel == null)
            {
                throw new NotFoundException(request);
            }

            return _mapper.Map<GetHotelDto>(hotel);
        }
    }
}
