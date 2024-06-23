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
using Accommo.Application.Handlers.External.Locations.Cities.CreateCity;

namespace Accommo.UnitTests.Tests.Cities.Commands.CreateCity;

public class CreateCityCommandHandlerTest : RequestHandlerTestBase<CreateCityCommand, GetCityExternalDto>
{
    private readonly Mock<IBaseWriteRepository<Country>> _countryMock = new();
    private readonly Mock<IBaseWriteRepository<City>> _cityMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<CreateCityCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public CreateCityCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(CreateCityCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateCityCommand, GetCityExternalDto> CommandHandler =>
        new CreateCityCommandHandler(_countryMock.Object, _cityMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_CreateCity_WhenCityNotExist()
    {
        //Arrange
        var command = new CreateCityCommand
        {
            CityId = Guid.NewGuid().ToString(),
            CityName = "Name city",
            CountryName = "Name country"
        };

        var country = new Country(Guid.NewGuid(), command.CountryName, true);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);
        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(null as City);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_CreateCity_WhenCityExist()
    {
        //Arrange
        var command = new CreateCityCommand
        {
            CityId = Guid.NewGuid().ToString(),
            CityName = "Name city",
            CountryName = "Name country"
        };

        var country = new Country(Guid.NewGuid(), command.CountryName, true);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);
        var city = new City(Guid.Parse(command.CityId), command.CityName, country.CountryId, true);
        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(city);
        city.UpdateIsActive(false);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowNotFound_WhenCountryNotFound()
    {
        //Arrange
        var command = new CreateCityCommand
        {
            CityId = Guid.NewGuid().ToString(),
            CityName = "Name city",
            CountryName = "Name country"
        };

        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(null as Country);

        //Act and Assert
        await AssertThrowNotFound(command);
    }
}