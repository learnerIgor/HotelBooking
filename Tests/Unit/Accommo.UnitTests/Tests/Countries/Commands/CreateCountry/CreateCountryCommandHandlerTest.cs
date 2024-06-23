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
using Accommo.Application.Handlers.External.Locations.Countries.CreateCountry;
using Accommo.Application.Handlers.External.Hotels;

namespace Accommo.UnitTests.Tests.Countries.Commands.CreateCountry;

public class CreateCountryCommandHandlerTest : RequestHandlerTestBase<CreateCountryCommand, GetCountryExternalDto>
{
    private readonly Mock<IBaseWriteRepository<Country>> _countryMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<CreateCountryCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public CreateCountryCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(CreateCountryCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateCountryCommand, GetCountryExternalDto> CommandHandler =>
        new CreateCountryCommandHandler(_countryMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_CreateCountry_WhenCountryNotExist()
    {
        //Arrange
        var command = new CreateCountryCommand
        {
            CountryId = Guid.NewGuid(),
            Name = "Name country",
        };

        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(null as Country);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_CreateCountry_WhenCountryExist()
    {
        //Arrange
        var command = new CreateCountryCommand
        {
            CountryId = Guid.NewGuid(),
            Name = "Name country",
        };

        var country = new Country(command.CountryId, command.Name, true);
        country.UpdateIsActive(false);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }
}