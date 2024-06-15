using AutoMapper;
using Booking.Application.Abstractions.Persistence.Repositories.Read;
using Booking.Domain;
using MediatR;
using Microsoft.Extensions.Logging;
using Booking.Application.Exceptions;
using Booking.Application.Utils;
using Booking.Application.Abstractions.Service;
using Booking.Domain.Enums;
using Booking.Application.Abstractions.Persistence.Repositories.Write;
using Booking.Application.Abstractions.ExternalProviders;
using Booking.Application.Caches;
using Booking.Application.Abstractions;
using System.Text.Json;

namespace Booking.Application.Handlers.Booking.Commands.CreateBooking
{
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, GetBookingDto>
    {
        private readonly IBaseReadRepository<Room> _room;
        private readonly IBaseReadRepository<ApplicationUser> _user;
        private readonly IBaseWriteRepository<Reservation> _reservation;
        private readonly IBaseWriteRepository<Payment> _payment;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBookingCommandHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        private readonly IBookingProvider _bookingProvider;
        private readonly ICleanBookingCacheService _cleanBookingCacheService;
        private readonly IMqEmailService _mqEmailService;
        private readonly IRoomProvider _roomProvider;

        public CreateBookingCommandHandler(
            IBaseReadRepository<Room> room, 
            IBaseReadRepository<ApplicationUser> user, 
            IBaseWriteRepository<Reservation> reservation,
            IBaseWriteRepository<Payment> payment,
            ICurrentUserService currentUserService, 
            IMapper mapper, 
            ILogger<CreateBookingCommandHandler> logger, 
            IBookingProvider bookingProvider,
            ICleanBookingCacheService cleanBookingCacheService,
            IMqEmailService mqEmailService,
            IRoomProvider roomProvider)
        {
            _room = room;
            _user = user;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _logger = logger;
            _reservation = reservation;
            _payment = payment;
            _bookingProvider = bookingProvider;
            _cleanBookingCacheService = cleanBookingCacheService;
            _mqEmailService = mqEmailService;
            _roomProvider = roomProvider;
        }

        public async Task<GetBookingDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var startDate = DateTime.Parse(request.StartDate);
            var endDate = DateTime.Parse(request.EndDate);
            if (startDate < DateTime.Now.Date || endDate < DateTime.Now.Date)
            {
                throw new BadOperationException("Incorrect dates");
            }

            var user = await _user.AsAsyncRead().SingleOrDefaultAsync(l => l.ApplicationUserId == _currentUserService.CurrentUserId && l.IsActive, cancellationToken);
            if (user == null) 
            {
                throw new BadOperationException($"User doesn't exist");
            }
            if (_currentUserService.CurrentUserId != user!.ApplicationUserId &&
                !_currentUserService.UserInRole(ApplicationUserRolesEnum.Admin))
            {
                throw new ForbiddenException();
            }

            var idRoomGuid = Guid.Parse(request.RoomId);
            var room = await _room.AsAsyncRead().SingleOrDefaultAsync(r => r.RoomId == idRoomGuid && r.IsActive, cancellationToken);
            if (room == null)
            {
                room = await _roomProvider.GetRoomAsync(request.RoomId, cancellationToken);
            }

            var isReservExist = await _reservation.AsAsyncRead().AnyAsync(e => e.RoomId == idRoomGuid && e.IsActive && ((startDate >= e.CheckInDate && startDate <= e.CheckOutDate) || 
                                                                                                                        (endDate >= e.CheckInDate && endDate <= e.CheckOutDate) || 
                                                                                                                        (startDate <= e.CheckInDate && endDate >= e.CheckOutDate))
                                                                                                                        , cancellationToken);

            if (isReservExist)
            {
                throw new BadOperationException($"You cannot book a room with ID {idRoomGuid} for this period of time. Please choose another room or change dates");
            }

            var amount = AmountUtil.CalculateAmount(startDate, endDate, room.RoomType.BaseCost);
            var entityPayment = new Payment(DateTime.Now, amount, false, true);
            var payment = await _payment.AddAsync(entityPayment, cancellationToken);
            
            var entityReservation = new Reservation(startDate, endDate, true, user.ApplicationUserId, room.RoomId, entityPayment.PaymentId);
            var reservation = await _reservation.AddAsync(entityReservation, cancellationToken);
            await _bookingProvider.AddBookingAsync(_currentUserService.Token, reservation, cancellationToken);

            SendEmailDto sendEmail = new()
            {
                ApplicationUserId = user.ApplicationUserId.ToString(),
                Login = user.Login,
                Email = user.Email,
                Hotel = room.Hotel.Name,
                RoomType = room.RoomType.Name,
                CheckIn = reservation.CheckInDate.ToString(),
                CheckOut = reservation.CheckOutDate.ToString()

            };
            await _mqEmailService.SendEmailMessage("sendEmail", JsonSerializer.Serialize(sendEmail));
            payment!.UpdateIsSendEmail(true);
            await _payment.UpdateAsync(payment, cancellationToken);

            _logger.LogInformation($"New reservation {reservation.ReservationId} created.");
            _cleanBookingCacheService.ClearListCaches();

            return _mapper.Map<GetBookingDto>(reservation);
        }
    }
}