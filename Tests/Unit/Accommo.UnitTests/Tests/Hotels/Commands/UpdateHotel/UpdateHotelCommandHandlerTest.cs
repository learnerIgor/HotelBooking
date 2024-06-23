using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Caches;
using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Hotels.UpdateHotel;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using AutoMapper;
using Core.Tests;
using Core.Tests.Fixtures;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit.Abstractions;
using Accommo.Domain;
using System.Linq.Expressions;

namespace Accommo.UnitTests.Tests.Hotels.Commands.UpdateHotel
{
    public class UpdateHotelCommandHandlerTest : RequestHandlerTestBase<UpdateHotelCommand, GetHotelExternalDto>
    {
        private readonly Mock<IBaseWriteRepository<Hotel>> _hotelMock = new();
        private readonly Mock<IBaseWriteRepository<City>> _cityMock = new();
        private readonly Mock<IBaseWriteRepository<Address>> _addressMock = new();
        private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
        private readonly Mock<ILogger<UpdateHotelCommandHandler>> _loggerMock = new();
        private readonly IMapper _mapper;
        public UpdateHotelCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(UpdateHotelCommand).Assembly).Mapper;
            _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
        }

        protected override IRequestHandler<UpdateHotelCommand, GetHotelExternalDto> CommandHandler =>
            new UpdateHotelCommandHandler(_hotelMock.Object, _addressMock.Object, _cityMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

        [Fact]
        public async Task Should_UpdateHotel_WhenAddresExist()
        {
            //Arrange
            var command = new UpdateHotelCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                Description = "Description",
                Rating = 3,
                Image = "https://image",
                AddressId = Guid.NewGuid(),
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

            var city = new City(Guid.NewGuid(), "City", Guid.NewGuid(), true);
            _cityMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
                .ReturnsAsync(city);

            var address = new Address(Guid.NewGuid(), "Street", "12", 24.24M, 45.4M, true, city.CityId);
            _addressMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Address, bool>>>(), default))
                .ReturnsAsync(address);
            _addressMock
                .Setup(p => p.AddAsync(address, default));

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", address, "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);
            hotel.UpdateName(command.Name);
            hotel.UpdateDescription(command.Description);
            hotel.UpdateAddres(command.AddressId);
            hotel.UpdateRating(command.Rating);
            hotel.UpdateImage(command.Image);


            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_UpdateHote_WhenAddresNotExist()
        {
            //Arrange
            var command = new UpdateHotelCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Name",
                Description = "Description",
                Rating = 3,
                Image = "https://image",
                AddressId = Guid.NewGuid(),
                Address = new GetAddressExternalDto
                {
                    Street = "Street",
                    HouseNumber = "12",
                    Latitude = 23.2M,
                    Longitude = 43.4M,
                    City = new GetCityExternalDto
                    {
                        CityId = Guid.NewGuid(),
                        Name = "NameCity",
                        Country = new GetCountryExternalDto
                        {
                            CountryId = Guid.NewGuid(),
                            Name = "NameCountry"
                        }
                    }
                }
            };

            var city = new City(Guid.NewGuid(), "City", Guid.NewGuid(), true);
            _cityMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<City, bool>>>(), default))
                .ReturnsAsync(city);

            var address = new Address(command.AddressId, command.Address.Street, command.Address.HouseNumber, command.Address.Latitude, command.Address.Longitude, true, command.Address.City.CityId);
            _addressMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Address, bool>>>(), default))
                .ReturnsAsync(null as Address);

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", address, "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);
            hotel.Address.SetIsActive(false);

            _cleanAccommoCacheService.ClearAllCaches();

            //Act and Assert
            await AssertNotThrow(command);
        }

        [Fact]
        public async Task Should_ThrowNotFount_When_HotelNotFound()
        {

            //Arrange
            var command = new UpdateHotelCommand
            {
                Id = Guid.NewGuid().ToString(),
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

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");
            _hotelMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(null as Hotel);

            //Act and Assert
            await AssertThrowNotFound(command);
        }
    }
}
