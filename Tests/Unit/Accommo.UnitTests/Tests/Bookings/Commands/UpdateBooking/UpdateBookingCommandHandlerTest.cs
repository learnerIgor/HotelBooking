using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Core.Tests;
using Core.Tests.Fixtures;
using MediatR;
using Moq;
using Accommo.Domain;
using Xunit.Abstractions;
using Accommo.Application.Caches;
using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Accommo.Application.Handlers.External.Bookings;
using Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking;

namespace Accommo.UnitTests.Tests.Bookings.Commands.UpdateBooking;

public class UpdateBookingCommandHandlerTest : RequestHandlerTestBase<UpdateBookingCommand, GetBookingDto>
{
    private readonly Mock<IBaseWriteRepository<Reservation>> _reservationMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<UpdateBookingCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public UpdateBookingCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(UpdateBookingCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<UpdateBookingCommand, GetBookingDto> CommandHandler =>
        new UpdateBookingCommandHandler(_reservationMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_UpdateBooking_WhenRequestIsCorrect()
    {
        //Arrange
        var command = new UpdateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            StartDate = "2024-08-01",
            EndDate = "2024-08-11",
        };

        var reservation = new Reservation(Guid.NewGuid(), DateTime.Now, DateTime.Now.AddDays(10), true, Guid.NewGuid());
        _reservationMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Reservation, bool>>>(), default))
            .ReturnsAsync(reservation);
        reservation.UpdateCheckInDate(DateTime.Parse(command.StartDate));
        reservation.UpdateCheckOutDate(DateTime.Parse(command.EndDate));

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowBadOperation_WhenStartDateLessThanNowDate()
    {
        //Arrange
        var command = new UpdateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            StartDate = "2024-05-01",
            EndDate = "2024-08-11",
        };

        //Act and Assert
        await AssertThrowBadOperation(command, $"Incorrect dates");
    }

    [Fact]
    public async Task Should_ThrowBadOperation_WhenEndDateLessThanNowDate()
    {
        //Arrange
        var command = new UpdateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            StartDate = "2024-08-01",
            EndDate = "2024-05-11",
        };

        //Act and Assert
        await AssertThrowBadOperation(command, $"Incorrect dates");
    }

    [Fact]
    public async Task Should_ThrowNotFound_WhenReservationNotFound()
    {
        //Arrange
        var command = new UpdateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            StartDate = "2024-08-01",
            EndDate = "2024-08-11",
        };

        _reservationMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Reservation, bool>>>(), default))
            .ReturnsAsync(null as Reservation);

        //Act and Assert
        await AssertThrowNotFound(command);
    }
}