using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Hotels.CreateHotel;
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

namespace Accommo.UnitTests.Tests.Hotels.Commands.CreateHotel;

public class CreateHotelCommandHandlerTest : RequestHandlerTestBase<CreateHotelCommand, GetHotelExternalDto>
{
    private readonly Mock<IBaseWriteRepository<Hotel>> _hotelMock = new();
    private readonly Mock<IBaseWriteRepository<Country>> _countryMock = new();
    private readonly Mock<IBaseWriteRepository<City>> _cityMock = new();
    private readonly Mock<IBaseWriteRepository<Address>> _addressMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<CreateHotelCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public CreateHotelCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(CreateHotelCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateHotelCommand, GetHotelExternalDto> CommandHandler =>
        new CreateHotelCommandHandler(_hotelMock.Object, _countryMock.Object, _cityMock.Object, _addressMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_CreateHotel_WhenAddresExist()
    {
        //Arrange
        var command = new CreateHotelCommand
        {
            HotelId = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            Rating = 3,
            Image = "https://image",
            Address = new GetAddressExternalDto
            {
                Street = "Street",
                HouseNumber = "12",
                Latitude = 23.2M,
                Longitude = 43.4M,
                City = new GetCityExternalDto
                {
                    Name = "NameCity",
                    Country = new GetCountryExternalDto
                    {
                        Name = "NameCountry"
                    }
                }
            }
        };

        var country = new Country(Guid.NewGuid(), "Country", true);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);

        var city = new City(Guid.NewGuid(), "City", country.CountryId, true);
        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(city);

        var address = new Address(Guid.NewGuid(), "Street", "12", 24.24M, 45.4M, true, city.CityId);
        _addressMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Address, bool>>>(), default))
            .ReturnsAsync(address);
        _addressMock
            .Setup(p => p.AddAsync(address, default));

        var hotel = new Hotel(Guid.NewGuid(), "Hotel", address.AddressId, "description", 4, true, "https://dfsdfsdfg");
        _hotelMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
            .ReturnsAsync(hotel);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_CreateHotel_WhenAddresNotExist()
    {
        //Arrange
        var command = new CreateHotelCommand
        {
            HotelId = Guid.NewGuid(),
            Name = "Name",
            Description = "Description",
            Rating = 3,
            Image = "https://image",
            Address = new GetAddressExternalDto
            {
                Street = "Street",
                HouseNumber = "12",
                Latitude = 23.2M,
                Longitude = 43.4M,
                City = new GetCityExternalDto
                {
                    Name = "NameCity",
                    Country = new GetCountryExternalDto
                    {
                        Name = "NameCountry"
                    }
                }
            }
        };

        var country = new Country(Guid.NewGuid(), "Country", true);
        _countryMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Country, bool>>>(), default))
            .ReturnsAsync(country);

        var city = new City(Guid.NewGuid(), "City", country.CountryId, true);
        _cityMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
            .ReturnsAsync(city);

        var address = new Address(command.AddressId, command.Address.Street, command.Address.HouseNumber, command.Address.Latitude, command.Address.Longitude, true, Guid.NewGuid());
        _addressMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Address, bool>>>(), default))
            .ReturnsAsync(null as Address);
        _addressMock
            .Setup(p => p.AddAsync(address, default));

        var hotel = new Hotel(Guid.NewGuid(), "Hotel", address.AddressId, "description", 4, true, "https://dfsdfsdfg");
        _hotelMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
            .ReturnsAsync(hotel);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }
}