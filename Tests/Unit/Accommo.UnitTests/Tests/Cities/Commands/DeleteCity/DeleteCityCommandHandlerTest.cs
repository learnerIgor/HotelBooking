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
using Accommo.Application.Handlers.External.Locations.Countries.DeleteCountry;
using Accommo.Application.Handlers.External.Locations.Cities.DeleteCity;

namespace Accommo.UnitTests.Tests.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandHandlerTest : RequestHandlerTestBase<DeleteCityCommand, Unit>
    {
        private readonly Mock<IBaseWriteRepository<City>> _cityMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<DeleteCityCommandHandler>> _loggerMock = new();
        public DeleteCityCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<DeleteCityCommand, Unit> CommandHandler =>
            new DeleteCityCommandHandler(_cityMock.Object, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_DeleteCity_WhenRequestIsValid()
        {
            //Arrange
            var command = new DeleteCityCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var city = new City(Guid.Parse(command.Id), "Name city", Guid.NewGuid(), true);
            city.UpdateIsActive(false);
            _cityMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
                .ReturnsAsync(city);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFound_WhenRoomTypeNotFound()
        {
            //Arrange
            var command = new DeleteCityCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            _cityMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
                .ReturnsAsync(null as City);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
