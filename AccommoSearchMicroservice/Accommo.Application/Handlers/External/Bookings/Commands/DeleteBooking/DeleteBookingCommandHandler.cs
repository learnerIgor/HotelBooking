using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;
using Accommo.Application.Exceptions;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accommo.Application.Handlers.External.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand, Unit>
    {
        private readonly IBaseWriteRepository<Reservation> _reservation;
        private readonly ILogger<DeleteBookingCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public DeleteBookingCommandHandler(
            IBaseWriteRepository<Reservation> reservation,
            ILogger<DeleteBookingCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _reservation = reservation;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<Unit> Handle(DeleteBookingCommand request, CancellationToken cancellationToken)
        {
            var idGuid = Guid.Parse(request.Id);
            var reservation = await _reservation.AsAsyncRead().SingleOrDefaultAsync(e => e.ReservationId == idGuid, cancellationToken);
            if (reservation is null)
            {
                throw new NotFoundException(request);
            }
            reservation.UpdateIsActive(false);

            await _reservation.UpdateAsync(reservation, cancellationToken);
            _logger.LogWarning($"Reservation {reservation.ReservationId} deleted");
            _cleanAccommoCacheService.ClearAllCaches();

            return default;
        }
    }
}
