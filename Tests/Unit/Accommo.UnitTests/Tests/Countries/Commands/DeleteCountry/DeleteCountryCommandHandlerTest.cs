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

namespace Accommo.UnitTests.Tests.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandHandlerTest : RequestHandlerTestBase<DeleteCountryCommand, Unit>
    {
        private readonly Mock<IBaseWriteRepository<Country>> _countryMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<DeleteCountryCommandHandler>> _loggerMock = new();
        public DeleteCountryCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<DeleteCountryCommand, Unit> CommandHandler =>
            new DeleteCountryCommandHandler(_countryMock.Object, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_DeleteCountry_WhenRequestIsValid()
        {
            //Arrange
            var command = new DeleteCountryCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            var country = new Country(Guid.Parse(command.Id), "Name country", true);
            country.UpdateIsActive(false);
            _countryMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
                .ReturnsAsync(country);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFound_WhenCountryNotFound()
        {
            //Arrange
            var command = new DeleteCountryCommand
            {
                Id = Guid.NewGuid().ToString(),
            };

            _countryMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
                .ReturnsAsync(null as Country);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
