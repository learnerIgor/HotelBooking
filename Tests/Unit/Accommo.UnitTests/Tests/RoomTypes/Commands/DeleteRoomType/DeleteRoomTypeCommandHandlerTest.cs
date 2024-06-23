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
using Accommo.Application.Handlers.External.RoomTypes.DeleteRoomType;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommandHandlerTest : RequestHandlerTestBase<DeleteRoomTypeCommand, Unit>
    {
        private readonly Mock<IBaseWriteRepository<RoomType>> _roomTypeMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<DeleteRoomTypeCommandHandler>> _loggerMock = new();
        public DeleteRoomTypeCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<DeleteRoomTypeCommand, Unit> CommandHandler =>
            new DeleteRoomTypeCommandHandler(_roomTypeMock.Object, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_DeleteRoomType_WhenRequestIsValid()
        {
            //Arrange
            var command = new DeleteRoomTypeCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var roomType = new RoomType(Guid.Parse(command.Id), "Test name", 123454.5M, true);
            roomType.UpdateIsActive(false);
            _roomTypeMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
                .ReturnsAsync(roomType);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFound_WhenRoomTypeNotFound()
        {
            //Arrange
            var command = new DeleteRoomTypeCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var roomType = new RoomType(Guid.NewGuid(), "Test name", 123454.5M, true);
            _roomTypeMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
                .ReturnsAsync(null as RoomType);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
