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
using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Locations.Cities.UpdateCity;

namespace Accommo.UnitTests.Tests.Cities.Commands.UpdateCity;

public class UpdateCityCommandHandlerTest : RequestHandlerTestBase<UpdateCityCommand, GetCityExternalDto>
{
    private readonly Mock<IBaseWriteRepository<City>> _cityMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<UpdateCityCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public UpdateCityCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(UpdateCityCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<UpdateCityCommand, GetCityExternalDto> CommandHandler =>
        new UpdateCityCommandHandler(_cityMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_UpdateCity_WhenCommandIsValid()
    {
        //Arrange

        var payLoad = new UpdateCityCommandPayLoad { Name = "New name city" };

        var command = new UpdateCityCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name,
        };
        var city = new City(Guid.Parse(command.Id), "Name", Guid.NewGuid(), true);
        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(city);
        city.UpdateName(command.Name);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_BadOperation_WhenCityWithRequestNameExist()
    {
        //Arrange
        var payLoad = new UpdateCityCommandPayLoad { Name = "New name city" };

        var command = new UpdateCityCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name,
        };

        var city = new City(Guid.Parse(command.Id), command.Name, Guid.NewGuid(), true);
        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(city);
        _cityMock
            .Setup(p => p.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(true);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertThrowBadOperation(command, $"City with name { command.Name} already exists.");
    }

    [Fact]
    public async Task Should_ThrowNotFound_WhenCityNotFound()
    {
        //Arrange
        var payLoad = new UpdateCityCommandPayLoad { Name = "New name city" };

        var command = new UpdateCityCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name,
        };

        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(null as City);

        //Act and Assert
        await AssertThrowNotFound(command);
    }
}