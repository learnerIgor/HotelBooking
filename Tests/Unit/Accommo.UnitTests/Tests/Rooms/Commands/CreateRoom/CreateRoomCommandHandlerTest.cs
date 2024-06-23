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
using Accommo.Application.Handlers.External.Rooms.CreateRoom;
using Accommo.Application.Handlers.External.Rooms;

namespace Accommo.UnitTests.Tests.Rooms.Commands.CreateRoom;

public class CreateRoomCommandHandlerTest : RequestHandlerTestBase<CreateRoomCommand, GetRoomExternalDto>
{
    private readonly Mock<IBaseWriteRepository<Hotel>> _hotelMock = new();
    private readonly Mock<IBaseWriteRepository<Room>> _roomMock = new();
    private readonly Mock<IBaseWriteRepository<RoomType>> _roomTypeMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<CreateRoomCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public CreateRoomCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(CreateRoomCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateRoomCommand, GetRoomExternalDto> CommandHandler =>
        new CreateRoomCommandHandler(_hotelMock.Object, _roomTypeMock.Object, _roomMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_CreateHotel_WhenRequestIsValid()
    {
        //Arrange
        var roomType = new RoomType(Guid.NewGuid(), "NewRoomType", 1500.5M, true);
        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(roomType);

        var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
        _hotelMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
            .ReturnsAsync(hotel);

        var amenity = new AmenityRoom[] { new(1), new(2), new(3), new(4) };
        var room = new Room(1, 1, roomType.RoomTypeId, true, hotel.HotelId, amenity, "https://dfsdfsdfg");
        _roomMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
            .ReturnsAsync(room);

        var command = new CreateRoomCommand
        {
            RoomId = Guid.NewGuid(),
            Floor = 1,
            Number = 1000,
            RoomTypeId = Guid.NewGuid().ToString(),
            HotelId = Guid.NewGuid().ToString(),
            Image = "https://image",
            Amenities = [10, 20, 30]
        };

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowNotFound_When_RoomTypeNotFound()
    {
        //Arrange
        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(null as RoomType);

        var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
        _hotelMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
            .ReturnsAsync(hotel);

        var amenity = new AmenityRoom[] { new(1), new(2), new(3), new(4) };
        var room = new Room(1, 1, Guid.NewGuid(), true, hotel.HotelId, amenity, "https://dfsdfsdfg");
        _roomMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
            .ReturnsAsync(room);

        var command = new CreateRoomCommand
        {
            RoomId = Guid.NewGuid(),
            Floor = 1,
            Number = 1000,
            RoomTypeId = Guid.NewGuid().ToString(),
            HotelId = Guid.NewGuid().ToString(),
            Image = "https://image",
            Amenities = [10, 20, 30]
        };

        //Act and Assert
        await AssertThrowNotFound(command);
    }

    [Fact]
    public async Task Should_ThrowNotFound_When_HotelNotFound()
    {
        //Arrange
        var roomType = new RoomType(Guid.NewGuid(), "NewRoomType", 1500.5M, true);
        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(roomType);

        _hotelMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
            .ReturnsAsync(null as Hotel);

        var amenity = new AmenityRoom[] { new(1), new(2), new(3), new(4) };
        var room = new Room(1, 1, Guid.NewGuid(), true, Guid.NewGuid(), amenity, "https://dfsdfsdfg");
        _roomMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
            .ReturnsAsync(room);

        var command = new CreateRoomCommand
        {
            RoomId = Guid.NewGuid(),
            Floor = 1,
            Number = 1000,
            RoomTypeId = Guid.NewGuid().ToString(),
            HotelId = Guid.NewGuid().ToString(),
            Image = "https://image",
            Amenities = [10, 20, 30]
        };

        //Act and Assert
        await AssertThrowNotFound(command);
    }

    [Fact]
    public async Task Should_ThrowBadOperation_When_RoomExist()
    {
        //Arrange
        var roomType = new RoomType(Guid.NewGuid(), "NewRoomType", 1500.5M, true);
        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(roomType);

        var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
        _hotelMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
            .ReturnsAsync(hotel);

        var amenity = new AmenityRoom[] { new(1), new(2), new(3), new(4) };
        var room = new Room(1, 1, Guid.NewGuid(), true, Guid.NewGuid(), amenity, "https://dfsdfsdfg");
        _roomMock
            .Setup(p => p.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
            .ReturnsAsync(true);

        var command = new CreateRoomCommand
        {
            RoomId = Guid.NewGuid(),
            Floor = 1,
            Number = 1000,
            RoomTypeId = Guid.NewGuid().ToString(),
            HotelId = Guid.NewGuid().ToString(),
            Image = "https://image",
            Amenities = [10, 20, 30]
        };

        //Act and Assert
        await AssertThrowBadOperation(command, $"The room already exists in AccommoMicroservice.");
    }
}