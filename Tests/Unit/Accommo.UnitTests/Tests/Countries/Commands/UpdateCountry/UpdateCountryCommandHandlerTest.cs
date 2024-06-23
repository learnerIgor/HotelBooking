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
using Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry;

namespace Accommo.UnitTests.Tests.Countries.Commands.UpdateCountry;

public class UpdateCountryCommandHandlerTest : RequestHandlerTestBase<UpdateCountryCommand, GetCountryExternalDto>
{
    private readonly Mock<IBaseWriteRepository<Country>> _countryMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<UpdateCountryCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public UpdateCountryCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(UpdateCountryCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<UpdateCountryCommand, GetCountryExternalDto> CommandHandler =>
        new UpdateCountryCommandHandler(_countryMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_UpdateCountry_WhenCountryExist()
    {
        //Arrange
        var payLoad = new UpdateCountryCommandPayLoad { Name = "New name" };

        var command = new UpdateCountryCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name,
        };

        var country = new Country(Guid.Parse(command.Id), "Name", true);
        country.UpdateName(command.Name);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);
        _countryMock
            .Setup(c => c.UpdateAsync(country, default)).ReturnsAsync(country);

        _cleanAccommoCacheService.ClearAllCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowNotFount_WhenCountryNotFound()
    {
        //Arrange
        var payLoad = new UpdateCountryCommandPayLoad { Name = "New name" };

        var command = new UpdateCountryCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name,
        };

        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(null as Country);

        _cleanAccommoCacheService.ClearAllCaches();

        //Act and Assert
        await AssertThrowNotFound(command);
    }

    [Fact]
    public async Task Should_ThrowBadOperation_WhenCountryWithInputNameExist()
    {
        //Arrange
        var payLoad = new UpdateCountryCommandPayLoad { Name = "New name" };

        var command = new UpdateCountryCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name,
        };

        var country = new Country(Guid.Parse(command.Id), command.Name, true);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);
        _countryMock
            .Setup(p => p.AsAsyncRead().AnyAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(true);

        _cleanAccommoCacheService.ClearAllCaches();

        //Act and Assert
        await AssertThrowBadOperation(command, $"Country with name {command.Name} already exists.");
    }
}