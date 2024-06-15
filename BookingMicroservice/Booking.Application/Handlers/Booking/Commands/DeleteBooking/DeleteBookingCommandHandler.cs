using Booking.Application.Abstractions.ExternalProviders;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Application.Abstractions.Service;
using Booking.Application.Caches;
using Booking.Application.Exceptions;
using Booking.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Booking.Domain.Enums;

namespace Booking.Application.Handlers.Booking.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IBaseWriteRepository<Reservation> _reservation;
        private readonly IBaseReadRepository<ApplicationUser> _user;
        private readonly ILogger<DeleteBookingCommandHandler> _logger;
        private readonly ICleanBookingCacheService _cleanBookingCacheService;
        private readonly IBookingProvider _bookingProvider;
        private ICurrentUserService _currentUserService;

        public DeleteBookingCommandHandler(
            IBaseWriteRepository<Reservation> reservation,
            IBaseReadRepository<ApplicationUser> user,
            ILogger<DeleteBookingCommandHandler> logger, 
            ICleanBookingCacheService cleanBookingCacheService,
            IBookingProvider bookingProvider,
            ICurrentUserService currentUserService)
        {
            _user = user;
            _reservation = reservation;
            _logger = logger;
            _cleanBookingCacheService = cleanBookingCacheService;
            _bookingProvider = bookingProvider;
            _currentUserService = currentUserService;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _user.AsAsyncRead().SingleOrDefaultAsync(l => l.ApplicationUserId == _currentUserService.CurrentUserId && l.IsActive, cancellationToken);

            if (_currentUserService.CurrentUserId != user!.ApplicationUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }
            var idGuid = Guid.Parse(request.Id);
            var reservation = await _reservation.AsAsyncRead().SingleOrDefaultAsync(e => e.ReservationId == idGuid, cancellationToken);
            if (reservation is null)
            {
                throw new NotFoundException(request);
            }
            reservation.UpdateIsActive(false);
            reservation.Payment.UpdateIsActive(false);

            await _reservation.UpdateAsync(reservation, cancellationToken);
            await _bookingProvider.DeleteBookingAsync(_currentUserService.Token, idGuid, cancellationToken);
            _logger.LogWarning($"Reservation {reservation.ReservationId} deleted");
            _cleanBookingCacheService.ClearAllCaches();

            return default;
        }
    }
}
