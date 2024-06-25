using AutoMapper;
using Booking.Application.Abstractions.Caches;
using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Application.Abstractions.Service;
using Booking.Application.BaseRealizations;
using Booking.Application.Dtos;
using Booking.Application.Exceptions;
using Booking.Domain;
using Booking.Domain.Enums;

namespace Booking.Application.Handlers.Booking.Queries.GetUserBookings
{
    public class GetUserBookingsQueryHandler : BaseCashedForUserQuery<GetUserBookingsQuery, BaseListDto<GetBookingDto>>
    {
        private readonly IBaseReadRepository<Reservation> _reservations;
        private readonly IBaseReadRepository<ApplicationUser> _user;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetUserBookingsQueryHandler(
            IBaseReadRepository<Reservation> reservations,
            IBaseReadRepository<ApplicationUser> user,
            IMapper mapper, ICurrentUserService currentUserService, 
            IBookingListMemoryCache bookingListMemoryCache) : base(bookingListMemoryCache, currentUserService.CurrentUserId!.Value)
        {
            _reservations = reservations;
            _user = user;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public override async Task<BaseListDto<GetBookingDto>> SentQueryAsync(GetUserBookingsQuery request, CancellationToken cancellationToken)
        {
            var user = await _user.AsAsyncRead().SingleOrDefaultAsync(l => l.ApplicationUserId == _currentUserService.CurrentUserId && l.IsActive, cancellationToken);
            if (user == null)
            {
                throw new BadOperationException($"User doesn't exist");
            }
            var idGuid = Guid.Parse(request.ApplicationUserId);
            if (_currentUserService.CurrentUserId != idGuid &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            var reservations = await _reservations.AsAsyncRead().ToArrayAsync(i => i.ApplicationUserId == idGuid && i.IsActive, cancellationToken);
            if (reservations == null)
            {
                throw new NotFoundException(request);
            }

            var reservationsCount = await _reservations.AsAsyncRead().CountAsync(i => i.ApplicationUserId == idGuid, cancellationToken);

            var items = _mapper.Map<GetBookingDto[]>(reservations);
            return new BaseListDto<GetBookingDto>
            {
                Items = items,
                TotalCount = reservationsCount
            };
        }
    }
}
