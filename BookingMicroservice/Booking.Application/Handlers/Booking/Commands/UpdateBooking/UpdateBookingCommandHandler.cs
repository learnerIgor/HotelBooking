using AutoMapper;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Booking.Application.Exceptions;
using Booking.Application.Caches;
using Booking.Application.Abstractions.ExternalProviders;
using Booking.Application.Abstractions.Service;
using Booking.Domain.Enums;

namespace Booking.Application.Handlers.Booking.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, GetBookingDto>
    {
        private readonly IBaseWriteRepository<Reservation> _reservation;
        private readonly IBaseReadRepository<ApplicationUser> _user;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookingCommandHandler> _logger;
        private readonly ICleanBookingCacheService _cleanBookingCacheService;
        private readonly IBookingProvider _bookingProvider;
        private readonly ICurrentUserService _currentUserService;

        public UpdateBookingCommandHandler(
            IBaseWriteRepository<Reservation> reservation,
            IBaseReadRepository<ApplicationUser> user,
            IMapper mapper,
            ILogger<UpdateBookingCommandHandler> logger,
            ICleanBookingCacheService cleanBookingCacheService,
            IBookingProvider bookingProvider,
            ICurrentUserService currentUserService)
        {
            _reservation = reservation;
            _user = user;
            _mapper = mapper;
            _logger = logger;
            _cleanBookingCacheService = cleanBookingCacheService;
            _bookingProvider = bookingProvider;
            _currentUserService = currentUserService;
        }

        public async Task<GetBookingDto> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
            var user = await _user.AsAsyncRead().SingleOrDefaultAsync(l => l.ApplicationUserId == _currentUserService.CurrentUserId && l.IsActive, cancellationToken);

            if (_currentUserService.CurrentUserId != user!.ApplicationUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            var startDate = DateTime.Parse(request.StartDate);
            var endDate = DateTime.Parse(request.EndDate);
            if (startDate < DateTime.Now.Date || endDate < DateTime.Now.Date)
            {
                throw new BadOperationException("Incorrect dates");
            }

            var idGuidReserv = Guid.Parse(request.ReservationId);
            var reservation = await _reservation.AsAsyncRead().SingleOrDefaultAsync(h => h.ReservationId == idGuidReserv && h.IsActive, cancellationToken);
            if (reservation == null)
            {
                throw new NotFoundException(request);
            }

            var isReservExist = await _reservation.AsAsyncRead().AnyAsync(e => e.RoomId == reservation.RoomId && e.ReservationId != reservation.ReservationId && ((startDate >= e.CheckInDate && startDate <= e.CheckOutDate) ||
                                                                                                                  (endDate >= e.CheckInDate && endDate <= e.CheckOutDate) ||
                                                                                                                  (startDate <= e.CheckInDate && endDate >= e.CheckOutDate))
                                                                                                                  , cancellationToken);

            if (isReservExist)
            {
                throw new BadOperationException($"You cannot book a room with ID {reservation.RoomId} for this period of time. Please choose another room or change dates");
            }

            reservation.UpdateCheckInDate(startDate);
            reservation.UpdateCheckOutDate(endDate);

            var result = await _reservation.UpdateAsync(reservation, cancellationToken);
            await _bookingProvider.UpdateBookingAsync(_currentUserService.Token, request.ReservationId, reservation, cancellationToken);
            _logger.LogInformation($"Reservation {result.ReservationId} updated");
            _cleanBookingCacheService.ClearAllCaches();

            return _mapper.Map<GetBookingDto>(result);
        }
    }
}
