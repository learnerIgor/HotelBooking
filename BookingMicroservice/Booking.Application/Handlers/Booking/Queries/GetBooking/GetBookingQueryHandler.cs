using AutoMapper;
using Booking.Application.Abstractions.Caches;
using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Application.Abstractions.Service;
using Booking.Application.BaseRealizations;
using Booking.Application.Exceptions;
using Booking.Domain;
using Booking.Domain.Enums;

namespace Booking.Application.Handlers.Booking.Queries.GetBooking
{
    public class GetUserBookingQueryHandler : BaseCashedForUserQuery<GetBookingQuery, GetBookingDto>
    {
        private readonly IBaseReadRepository<Reservation> _reservation;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserBookingQueryHandler(IBaseReadRepository<Reservation> reservation, IMapper mapper, ICurrentUserService currentUserService, IBookingMemoryCache memoryCache) : base(memoryCache, currentUserService.CurrentUserId!.Value)
        {
            _reservation = reservation;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public override async Task<GetBookingDto> SentQueryAsync(GetBookingQuery request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.ReservationId);
            var reservation = await _reservation.AsAsyncRead().SingleOrDefaultAsync(i => i.ReservationId == idGuid && i.IsActive, cancellationToken);
            if (reservation == null)
            {
                throw new NotFoundException(request);
            }

            if (_currentUserService.CurrentUserId != reservation.ApplicationUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            return _mapper.Map<GetBookingDto>(reservation);
        }
    }
}
