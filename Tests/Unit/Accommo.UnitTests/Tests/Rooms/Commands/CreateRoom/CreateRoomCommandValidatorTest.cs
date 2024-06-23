using Accommo.Application.Handlers.External.Rooms.CreateRoom;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Rooms.Commands.CreateRoom
{
    public class CreateRoomCommandValidatorTest : ValidatorTestBase<CreateRoomCommand>
    {
        public CreateRoomCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateRoomCommand> TestValidator => TestFixture.Create<CreateRoomCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("dc7b523d-9642-4c27-b43e-a8cb6ac57e8d")]
        [FixtureInlineAutoData("423a2067-1f97-4a17-8b47-76a7b3784f1f")]
        [FixtureInlineAutoData("be67085b-e0df-4292-b812-ab783dbd9773")]
        public void Should_BeValid_When_CorrectRoomId(string roomId)
        {
            // arrange
            var idRoom = Guid.TryParse(roomId, out _);

            // act & assert
            Assert.True(idRoom);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("adfsdfsgsfgd")]
        [FixtureInlineAutoData("afsdfgsdfsdfsdf")]
        public void Should_NotBeValid_When_IncorrectRoomId(string roomId)
        {
            // arrange
            var idRoom = Guid.TryParse(roomId, out _);

            // act & assert
            Assert.False(idRoom);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(33)]
        [FixtureInlineAutoData(100)]
        public void Should_BeValid_When_FloorIsValid(int floor)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = floor,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(101)]
        public void Should_NotBeValid_When_IncorrectFloor(int floor)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = floor,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(1500)]
        [FixtureInlineAutoData(7000)]
        public void Should_BeValid_When_NumberIsValid(int number)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = number,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("1234567")]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(7001)]
        public void Should_NotBeValid_When_IncorrectNumber(int number)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = number,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("e406872e-de9c-4dae-a79f-1a2b692b49b4")]
        [FixtureInlineAutoData("35a84a29-78ff-4979-9280-9d9f85590201")]
        public void Should_BeValid_When_RoomTypeIdIsValid(string roomTypeId)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = roomTypeId.ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("gfdghfghfghfgh")]
        [FixtureInlineAutoData("tgrftyty5645")]
        public void Should_NotBeValid_When_IncorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = roomTypeId.ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("e406872e-de9c-4dae-a79f-1a2b692b49b4")]
        [FixtureInlineAutoData("35a84a29-78ff-4979-9280-9d9f85590201")]
        public void Should_BeValid_When_HotelIdIsValid(string hotelId)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = hotelId.ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("gfdghfghfghfgh")]
        [FixtureInlineAutoData("tgrftyty5645")]
        public void Should_NotBeValid_When_IncorrectHotelId(string hotelId)
        {
            // arrange
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = hotelId.ToString(),
                Image = "http://image",
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
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
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = image,
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
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
            var command = new CreateRoomCommand
            {
                RoomId = Guid.NewGuid(),
                Floor = 1,
                Number = 1,
                RoomTypeId = Guid.NewGuid().ToString(),
                HotelId = Guid.NewGuid().ToString(),
                Image = image,
                Amenities = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10]
            };

            // act & assert
            AssertNotValid(command);
        }

    }
}
