using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Caches;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Core.Tests;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using Accommo.Domain;
using System.Linq.Expressions;
using Accommo.Application.Handlers.External.Rooms.DeleteRoom;

namespace Accommo.UnitTests.Tests.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandHandlerTest : RequestHandlerTestBase<DeleteRoomCommand, Unit>
    {
        private readonly Mock<IBaseWriteRepository<Room>> _roomMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<DeleteRoomCommandHandler>> _loggerMock = new();
        public DeleteRoomCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<DeleteRoomCommand, Unit> CommandHandler =>
            new DeleteRoomCommandHandler(_roomMock.Object, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_DeleteRoom_WhenRequestIsValid()
        {
            //Arrange
            var command = new DeleteRoomCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            int[] amenityRooms = [1, 2, 30];
            var room = new Room(Guid.Parse(command.Id), 1, 123, Guid.NewGuid(), true, Guid.NewGuid(), amenityRooms, "https://dfsdfsdfg");
            room.UpdateIsActive(false);
            _roomMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(room);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFound_WhenRoomNotFound()
        {
            //Arrange
            var command = new DeleteRoomCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            AmenityRoom[] amenityRooms = [new(1), new(2), new(30)];
            var room = new Room(1, 123, Guid.NewGuid(), true, Guid.NewGuid(), amenityRooms, "https://dfsdfsdfg");
            _roomMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(null as Room);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
