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
using Accommo.Application.Handlers.External.Bookings.Commands.CreateBooking;
using Accommo.Application.Handlers.External.Bookings;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;

namespace Accommo.UnitTests.Tests.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandlerTest : RequestHandlerTestBase<CreateBookingCommand, GetBookingDto>
{
    private readonly Mock<IBaseReadRepository<Room>> _roomMock = new();
    private readonly Mock<IBaseWriteRepository<Reservation>> _reservationMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<CreateBookingCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public CreateBookingCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(CreateBookingCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateBookingCommand, GetBookingDto> CommandHandler =>
        new CreateBookingCommandHandler(_roomMock.Object, _reservationMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_CreateBooking_WhenRequestIsCorrect()
    {
        //Arrange
        var command = new CreateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            CheckInDate = "2024-08-01",
            CheckOutDate = "2024-08-11",
            IsActive = true,
            RoomId = Guid.NewGuid().ToString()
        };

        var room = new Room(Guid.NewGuid(), 1, 2, Guid.NewGuid(), true, Guid.NewGuid(), [1, 2, 3, 4], "https://image");
        _roomMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
            .ReturnsAsync(room);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowBadOperation_WhenCheckInDateLessThanNowDate()
    {
        //Arrange
        var command = new CreateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            CheckInDate = "2024-05-11",
            CheckOutDate = "2024-08-20",
            IsActive = true,
            RoomId = Guid.NewGuid().ToString()
        };

        //Act and Assert
        await AssertThrowBadOperation(command, $"Incorrect dates");
    }

    [Fact]
    public async Task Should_ThrowBadOperation_WhenCheckOutDateLessThanNowDate()
    {
        //Arrange
        var command = new CreateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            CheckInDate = "2024-08-11",
            CheckOutDate = "2024-06-20",
            IsActive = true,
            RoomId = Guid.NewGuid().ToString()
        };

        //Act and Assert
        await AssertThrowBadOperation(command, $"Incorrect dates");
    }

    [Fact]
    public async Task Should_ThrowNotFound_WhenRoomNotFound()
    {
        //Arrange
        var command = new CreateBookingCommand
        {
            ReservationId = Guid.NewGuid().ToString(),
            CheckInDate = "2024-08-01",
            CheckOutDate = "2024-08-10",
            IsActive = true,
            RoomId = Guid.NewGuid().ToString()
        };

        _roomMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
            .ReturnsAsync(null as Room);

        //Act and Assert
        await AssertThrowNotFound(command);
    }
}