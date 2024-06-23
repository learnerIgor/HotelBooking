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
using Accommo.Application.Handlers.External.Hotels.DeleteHotel;

namespace Accommo.UnitTests.Tests.Hotels.Commands.DeleteHotel
{
    public class DeleteHotelCommandHandlerTest : RequestHandlerTestBase<DeleteHotelCommand, Unit>
    {
        private readonly Mock<IBaseWriteRepository<Hotel>> _hotelMock = new();
        private readonly Mock<IBaseWriteRepository<Address>> _addressMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<DeleteHotelCommandHandler>> _loggerMock = new();
        public DeleteHotelCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<DeleteHotelCommand, Unit> CommandHandler =>
            new DeleteHotelCommandHandler(_hotelMock.Object, _addressMock.Object, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_DeleteHotel_WhenRequestIsValid()
        {
            //Arrange
            var command = new DeleteHotelCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var address = new Address(Guid.NewGuid(), "Street", "12", 24.24M, 45.4M, true, Guid.NewGuid());
            _addressMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Address, bool>>>(), default))
                .ReturnsAsync(address);
            address.SetIsActive(false);

            var hotel = new Hotel(Guid.Parse(command.Id), "Hotel", address.AddressId, "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);
            hotel.UpdateIsActive(false);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFount_When_HotelNotFound()
        {

            //Arrange
            var command = new DeleteHotelCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var address = new Address(Guid.NewGuid(), "Street", "12", 24.24M, 45.4M, true, Guid.NewGuid());
            _addressMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Address, bool>>>(), default))
                .ReturnsAsync(address);

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", address.AddressId, "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(null as Hotel);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
