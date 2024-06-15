using Booking.Application.Abstractions.Caches;
using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Application.Abstractions.Service;
using Booking.Application.BaseRealizations;
using Booking.Application.Exceptions;
using Booking.Domain;
using Booking.Domain.Enums;

namespace Booking.Application.Handlers.Booking.Queries.GetBookingsCount
{
    internal class GetBookingsCountQueryHandler : BaseCashedForUserQuery<GetBookingsCountQuery, int>
    {
        private readonly IBaseReadRepository<Reservation> _bookings;
        private readonly ICurrentUserService _currentUserService;

        public GetBookingsCountQueryHandler(
            IBaseReadRepository<Reservation> users, 
            ICurrentUserService currentUserService, 
            IBookingCountMemoryCache cache) : base(cache, currentUserService.CurrentUserId!.Value)
        {
            _bookings = users;
            _currentUserService = currentUserService;
        }

        public override async Task<int> SentQueryAsync(GetBookingsCountQuery request, CancellationToken cancellationToken)
        {
            if (!_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }
            return await _bookings.AsAsyncRead().CountAsync(ListBookingWhere.Where(request), cancellationToken);
        }
    }
}