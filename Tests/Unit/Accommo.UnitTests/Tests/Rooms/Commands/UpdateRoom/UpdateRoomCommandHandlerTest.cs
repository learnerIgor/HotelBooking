using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Caches;
using Accommo.Application.Handlers.External.Hotels.UpdateHotel;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using AutoMapper;
using Core.Tests;
using Core.Tests.Fixtures;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using Accommo.Domain;
using System.Linq.Expressions;
using Accommo.Application.Handlers.External.Rooms.UpdateRoom;
using Accommo.Application.Handlers.External.Rooms;

namespace Accommo.UnitTests.Tests.Rooms.Commands.UpdateRoom
{
    public class UpdateRoomCommandHandlerTest : RequestHandlerTestBase<UpdateRoomCommand, GetRoomExternalDto>
    {
        private readonly Mock<IBaseWriteRepository<Hotel>> _hotelMock = new();
        private readonly Mock<IBaseWriteRepository<Room>> _roomMock = new();
        private readonly Mock<IBaseWriteRepository<RoomType>> _roomTypeMock = new();
        private readonly Mock<IBaseWriteRepository<AmenityRoom>> _amenityRoomMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<UpdateRoomCommandHandler>> _loggerMock = new();
        private readonly IMapper _mapper;
        public UpdateRoomCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(UpdateHotelCommand).Assembly).Mapper;
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<UpdateRoomCommand, GetRoomExternalDto> CommandHandler =>
            new UpdateRoomCommandHandler(_roomTypeMock.Object, _roomMock.Object, _amenityRoomMock.Object, _hotelMock.Object,   _mapper, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_UpdateHotel_WhenRequestIsValid()
        {
            //Arrange
            var command = new UpdateRoomCommand(Guid.NewGuid().ToString(), new UpdateRoomPayload
            {
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://sfdsdfsd",
                Amenities = [1, 2, 3, 4, 5]
            });

            var roomType = new RoomType(Guid.Parse(command.RoomTypeId), "NewRoomType", 1500.5M, true);
            _roomTypeMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
                .ReturnsAsync(roomType);

            var hotel = new Hotel(Guid.Parse(command.HotelId), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);

            var amenity = new AmenityRoom[] { new(1), new(2), new(3), new(4) };
            _amenityRoomMock
                .Setup(p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<AmenityRoom, bool>>>(), default))
                .ReturnsAsync(amenity);
            _amenityRoomMock
                .Setup(a => a.RemoveRangeAsync(amenity, default)).ReturnsAsync(1);

            var room = new Room(1, 1, Guid.Parse(command.RoomTypeId), true, Guid.Parse(command.HotelId), amenity, "https://dfsdfsdfg");
            _roomMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(room);
            room.UpdateFloor(command.Floor);
            room.UpdateNumber(command.Number);
            room.UpdateRoomType(Guid.Parse(command.RoomTypeId));
            room.UpdateHotel(Guid.Parse(command.HotelId));
            room.UpdateImage(command.Image);
            room.UpdateAmenities(command.Amenities);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowBadOperation_When_RoomTypeNotFound()
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

            var command = new UpdateRoomCommand(Guid.NewGuid().ToString(), new UpdateRoomPayload
            {
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://sfdsdfsd",
                Amenities = [1, 2, 3, 4, 5]
            });

            //Act and Assert
            await AssertThrowBadOperation(command, $"There is no type of number called {command.RoomTypeId} in AccommoMicroservice.");
        }

        [Fact]
        public async Task Should_ThrowBadOperation_When_HotelNotFound()
        {
            //Arrange
            var command = new UpdateRoomCommand(Guid.NewGuid().ToString(), new UpdateRoomPayload
            {
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://sfdsdfsd",
                Amenities = [1, 2, 3, 4, 5]
            });

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

            //Act and Assert
            await AssertThrowBadOperation(command, $"There is no hotel with id {command.HotelId} in AccommoMicroservice.");
        }

        [Fact]
        public async Task Should_ThrowBadOperation_When_RoomFromRequestExist()
        {
            //Arrange
            var command = new UpdateRoomCommand(Guid.NewGuid().ToString(), new UpdateRoomPayload
            {
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://sfdsdfsd",
                Amenities = [1, 2, 3, 4, 5]
            });

            var roomType = new RoomType(Guid.NewGuid(), "NewRoomType", 1500.5M, true);
            _roomTypeMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
                .ReturnsAsync(roomType);

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);

            _roomMock
                .Setup(p => p.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(true);

            //Act and Assert
            await AssertThrowBadOperation(command, $"The room already exists in AccommoMicroservice");
        }

        [Fact]
        public async Task Should_ThrowNotFount_When_RoomNotFound()
        {
            //Arrange
            var command = new UpdateRoomCommand(Guid.NewGuid().ToString(), new UpdateRoomPayload
            {
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://sfdsdfsd",
                Amenities = [1, 2, 3, 4, 5]
            });

            var roomType = new RoomType(Guid.NewGuid(), "NewRoomType", 1500.5M, true);
            _roomTypeMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
                .ReturnsAsync(roomType);

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);

            var amenity = new AmenityRoom[] { new(1), new(2), new(3), new(4) };
            _amenityRoomMock
                .Setup(p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<AmenityRoom, bool>>>(), default))
                .ReturnsAsync(amenity);

            var room = new Room(1, 1, Guid.NewGuid(), true, Guid.NewGuid(), amenity, "https://dfsdfsdfg");
            _roomMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(null as Room);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
