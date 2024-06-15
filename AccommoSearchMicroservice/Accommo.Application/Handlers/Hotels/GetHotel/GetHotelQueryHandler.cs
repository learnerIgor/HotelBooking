using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.BaseRealizations;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using Accommo.Application.Dtos.Hotels;
using Accommo.Application.Abstractions.Caches.Hotels;

namespace Accommo.Application.Handlers.Hotels.GetHotel
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
