using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Exceptions;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingCommand, GetBookingDto>
    {
        private readonly IBaseWriteRepository<Reservation> _reservation;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBookingCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public UpdateBookingCommandHandler(
            IBaseWriteRepository<Reservation> reservation,
            IMapper mapper,
            ILogger<UpdateBookingCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _reservation = reservation;
            _mapper = mapper;
            _logger = logger;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetBookingDto> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
        {
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

            reservation.UpdateCheckInDate(startDate);
            reservation.UpdateCheckOutDate(endDate);

            var result = await _reservation.UpdateAsync(reservation, cancellationToken);
            _logger.LogInformation($"Reservation {request.ReservationId} updated");
            _cleanAccommoCacheService.ClearAllCaches();

            return _mapper.Map<GetBookingDto>(result);
        }
    }
}
