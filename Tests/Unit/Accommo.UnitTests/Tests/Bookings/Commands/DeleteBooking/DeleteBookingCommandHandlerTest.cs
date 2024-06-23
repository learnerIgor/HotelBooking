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
using Accommo.Application.Handlers.External.Bookings.Commands.DeleteBooking;

namespace Accommo.UnitTests.Tests.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandHandlerTest : RequestHandlerTestBase<DeleteBookingCommand, Unit>
    {
        private readonly Mock<IBaseWriteRepository<Reservation>> _reservationMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<DeleteBookingCommandHandler>> _loggerMock = new();
        public DeleteBookingCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<DeleteBookingCommand, Unit> CommandHandler =>
            new DeleteBookingCommandHandler(_reservationMock.Object, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_DeleteBooking_WhenRequestIsValid()
        {
            //Arrange
            var command = new DeleteBookingCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var reservation = new Reservation(Guid.Parse(command.Id), DateTime.Now, DateTime.Now.AddDays(10), true, Guid.NewGuid());
            reservation.UpdateIsActive(false);
            _reservationMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Reservation, bool>>>(), default))
                .ReturnsAsync(reservation);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFound_WhenRoomNotFound()
        {
            //Arrange
            var command = new DeleteBookingCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var reservation = new Reservation(Guid.Parse(command.Id), DateTime.Now, DateTime.Now.AddDays(10), true, Guid.NewGuid());
            _reservationMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Reservation, bool>>>(), default))
                .ReturnsAsync(null as Reservation);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
