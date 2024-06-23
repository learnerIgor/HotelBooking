using Accommo.Application.Handlers.External.Hotels;
using Accommo.Application.Handlers.External.Hotels.CreateHotel;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Hotels.Commands.CreateHotel
{
    public class CreateHotelCommandValidatorTest : ValidatorTestBase<CreateHotelCommand>
    {
        public CreateHotelCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateHotelCommand> TestValidator => TestFixture.Create<CreateHotelCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "Name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                { 
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto 
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        { 
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("cd0b8891-cd0a-4786-971c-9a069e751171")]
        [FixtureInlineAutoData("51e1c4a0-e1c5-41cb-96ba-6b960540d70c")]
        [FixtureInlineAutoData("95bbc616-f7bf-4ef4-bebf-526ae4e3b928")]
        public void Should_BeValid_When_HotelIdIsValid(string hotelId)
        {
            // arrange
            var idHotel = Guid.TryParse(hotelId, out _);

            // act & assert
            Assert.True(idHotel);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("adfsdfsgsfgd")]
        [FixtureInlineAutoData("afsdfgsdfsdfsdf")]
        public void Should_NotBeValid_When_IncorrectHotelId(string hotelId)
        {
            // arrange
            var idHotel = Guid.TryParse(hotelId, out _);

            // act & assert
            Assert.False(idHotel);
        }

        [Theory]
        [FixtureInlineAutoData("null")]
        [FixtureInlineAutoData("name")]
        [FixtureInlineAutoData("1234567890-=asdfghjkl;'1][poiuytrewqzxcvbnm,./q2e4")]
        public void Should_BeValid_When_NameIsValid(string name)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = name,
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas")]
        public void Should_NotBeValid_When_IncorrectName(string name)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = name,
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("null123456")]
        [FixtureInlineAutoData("1234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./8" +
            "1234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./81234567890-=qwertyuiop[]asdfghjkl1;'2z3xcv4bnm,./8")]
        public void Should_BeValid_When_DescriptionIsValid(string description)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "Name",
                Description = description,
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("1234567")]
        [FixtureInlineAutoData("1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas1234567890qwertyuiop[]';lkjhgfdsazxcvbnm,./qwertyuikas")]
        public void Should_NotBeValid_When_IncorrectDescription(string description)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "Name",
                Description = description,
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(3)]
        [FixtureInlineAutoData(5)]
        public void Should_BeValid_When_RatingIsValid(int rating)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = rating,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(-1)]
        [FixtureInlineAutoData(6)]
        public void Should_NotBeValid_When_IncorrectRating(int rating)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = rating,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("http://sfdsdfsd")]
        [FixtureInlineAutoData("https://sfdsdfsd")]
        public void Should_BeValid_When_ImageIsValid(string image)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = image,
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("dfsdfsd")]
        [FixtureInlineAutoData("htp:/sfdsdfsd")]
        [FixtureInlineAutoData("http:/sfdsdfsd")]
        public void Should_NotBeValid_When_IncorrectImage(string image)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = image,
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("dfd")]
        [FixtureInlineAutoData("1234567890qwertyuiop[]asdfghjkl;'zxcvbnm,./135790-")]

        public void Should_BeValid_When_StreetIsValid(string street)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = street,
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("df")]
        [FixtureInlineAutoData("1234567890qwertyuiop[]asdfghjkl;'zxcvbnm,./135790-=087")]
        [FixtureInlineAutoData(1)]
        public void Should_NotBeValid_When_IncorrectStreet(string street)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = street,
                    HouseNumber = "123",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("1")]
        [FixtureInlineAutoData("q1w2e3r4t5")]
        public void Should_BeValid_When_HouseNumberIsValid(string hsouseNumber)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = hsouseNumber,
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("1234567890qwerty")]
        public void Should_NotBeValid_When_IncorrectHouseNumber(string hsouseNumber)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = hsouseNumber,
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(-90.00)]
        [FixtureInlineAutoData(0.00)]
        [FixtureInlineAutoData(90.00)]
        public void Should_BeValid_When_LatitudeIsValid(decimal latitude)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = latitude,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(-100.00)]
        [FixtureInlineAutoData(100.00)]
        public void Should_NotBeValid_When_IncorrectLatitude(decimal latitude)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = latitude,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(-180.00)]
        [FixtureInlineAutoData(0.00)]
        [FixtureInlineAutoData(180.00)]
        public void Should_BeValid_When_LongitudeIsValid(decimal longitude)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = 45.45M,
                    Longitude = longitude,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(-190.00)]
        [FixtureInlineAutoData(190.00)]
        public void Should_NotBeValid_When_IncorrectLongitude(decimal longitude)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = 45.45M,
                    Longitude = longitude,
                    City = new GetCityExternalDto
                    {
                        Name = "City",
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("123")]
        [FixtureInlineAutoData("q1w2e3r4t5y6u7i8o9p0[-]=a1s2d3f4g5h6j7k8l9;0'-c=z1")]
        public void Should_BeValid_When_CityNameIsValid(string cityName)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = cityName,
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("12")]
        [FixtureInlineAutoData("1234567890-=qwertyuiopasd1234567890-=qwertyuiopasda")]
        public void Should_NotBeValid_When_IncorrectCityName(string cityName)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = cityName,
                        Country = new GetCountryExternalDto
                        {
                            Name = "Country"
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("123")]
        [FixtureInlineAutoData("q1w2e3r4t5y6u7i8o9p0[-]=a1s2d3f4g5h6j7k8l9;0'-c=z1")]
        public void Should_BeValid_When_CountryNameIsValid(string countryName)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "cityName",
                        Country = new GetCountryExternalDto
                        {
                            Name = countryName
                        }
                    }
                }
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("12")]
        [FixtureInlineAutoData("1234567890-=qwertyuiopasd1234567890-=qwertyuiopasda")]
        public void Should_NotBeValid_When_IncorrectCountryName(string countryName)
        {
            // arrange
            var command = new CreateHotelCommand
            {
                HotelId = Guid.NewGuid(),
                Name = "name",
                Description = "Description",
                Rating = 2,
                Image = "http://image",
                Address = new GetAddressExternalDto
                {
                    Street = "street",
                    HouseNumber = "12",
                    Latitude = 45.45M,
                    Longitude = 45.45M,
                    City = new GetCityExternalDto
                    {
                        Name = "cityName",
                        Country = new GetCountryExternalDto
                        {
                            Name = countryName
                        }
                    }
                }
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
