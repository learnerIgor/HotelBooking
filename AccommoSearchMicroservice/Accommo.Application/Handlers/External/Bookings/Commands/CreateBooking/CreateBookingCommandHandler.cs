using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Accommo.Application.Exceptions;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Accommo.Application.Caches;

namespace Accommo.Application.Handlers.External.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, GetBookingDto>
    {
        private readonly IBaseReadRepository<Room> _room;
        private readonly IBaseWriteRepository<Reservation> _reservation;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookingCommandHandler> _logger;
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;

        public CreateBookingCommandHandler(
            IBaseReadRepository<Room> room,
            IBaseWriteRepository<Reservation> reservation,
            IMapper mapper,
            ILogger<CreateBookingCommandHandler> logger,
            ICleanAccommoCacheService cleanAccommoCacheService)
        {
            _room = room;
            _mapper = mapper;
            _logger = logger;
            _reservation = reservation;
            _cleanAccommoCacheService = cleanAccommoCacheService;
        }

        public async Task<GetBookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Parse(request.CheckInDate);
            var endDate = DateTime.Parse(request.CheckOutDate);
            if (startDate < DateTime.Now.Date || endDate < DateTime.Now.Date)
            {
                throw new BadOperationException("Incorrect dates");
            }

            var idRoomGuid = Guid.Parse(request.RoomId);
            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomId == idRoomGuid && r.IsActive, cancellationToken);
            if (room == null)
            {
                throw new NotFoundException($"Room with id {idRoomGuid} doesn't exist");
            }

            var idReservationGuid = Guid.Parse(request.ReservationId);
            var entityReservation = new Reservation(idReservationGuid, startDate, endDate, request.IsActive, idRoomGuid);

            var reservation = await _reservation.AddAsync(entityReservation, cancellationToken);
            _logger.LogInformation($"New reservation {reservation.ReservationId} created.");
            _cleanAccommoCacheService.ClearListCaches();

            return _mapper.Map<GetBookingDto>(reservation);
        }
    }
}
