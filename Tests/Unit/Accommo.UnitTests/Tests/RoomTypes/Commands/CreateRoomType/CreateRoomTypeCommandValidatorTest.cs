using Accommo.Application.Handlers.External.RoomTypes.CreateRoomType;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.CreateRoomType
{
    public class CreateRoomTypeCommandValidatorTest : ValidatorTestBase<CreateRoomTypeCommand>
    {
        public CreateRoomTypeCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateRoomTypeCommand> TestValidator => TestFixture.Create<CreateRoomTypeCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new CreateRoomTypeCommand
            {
                RoomTypeId = Guid.NewGuid(),
                Name = "Name of room type",
                BaseCost = 1500.5M,
                IsActive = true,
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("dc7b523d-9642-4c27-b43e-a8cb6ac57e8d")]
        [FixtureInlineAutoData("423a2067-1f97-4a17-8b47-76a7b3784f1f")]
        [FixtureInlineAutoData("be67085b-e0df-4292-b812-ab783dbd9773")]
        public void Should_BeValid_When_CorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var idRoomType = Guid.TryParse(roomTypeId, out _);

            // act & assert
            Assert.True(idRoomType);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("adfsdfsgsfgd")]
        [FixtureInlineAutoData("afsdfgsdfsdfsdf")]
        public void Should_NotBeValid_When_IncorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var idRoom = Guid.TryParse(roomTypeId, out _);

            // act & assert
            Assert.False(idRoom);
        }

        [Theory]
        [FixtureInlineAutoData("name")]
        [FixtureInlineAutoData("name new name")]
        [FixtureInlineAutoData("name1qw3ername1qw3ername1qw3ername1qw3ername1qw3er")]
        [FixtureInlineAutoData(100)]
        public void Should_BeValid_When_NameIsValid(string name)
        {
            // arrange
            var command = new CreateRoomTypeCommand
            {
                RoomTypeId = Guid.NewGuid(),
                Name = name,
                BaseCost = 1500.5M,
                IsActive = true,
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("name1qw3ername1qw3ername1qw3ername1qw3ername1qw3er1")]
        public void Should_NotBeValid_When_IncorrectName(string name)
        {
            // arrange
            var command = new CreateRoomTypeCommand
            {
                RoomTypeId = Guid.NewGuid(),
                Name = name,
                BaseCost = 1500.5M,
                IsActive = true,
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(500.2)]
        [FixtureInlineAutoData(1000.78)]
        [FixtureInlineAutoData(100)]
        public void Should_BeValid_When_BaseCostIsValid(decimal baseCost)
        {
            // arrange
            var command = new CreateRoomTypeCommand
            {
                RoomTypeId = Guid.NewGuid(),
                Name = "Name of room type",
                BaseCost = baseCost,
                IsActive = true,
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(null)]
        public void Should_NotBeValid_When_IncorrectBaseCost(decimal baseCost)
        {
            // arrange
            var command = new CreateRoomTypeCommand
            {
                RoomTypeId = Guid.NewGuid(),
                Name = "Name of room type",
                BaseCost = baseCost,
                IsActive = true,
            };

            // act & assert
            AssertNotValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(true)]
        [FixtureInlineAutoData(false)]
        public void Should_BeValid_When_IsActiveIsValid(bool isActive)
        {
            // arrange
            var command = new CreateRoomTypeCommand
            {
                RoomTypeId = Guid.NewGuid(),
                Name = "Name of room type",
                BaseCost = 12324.5M,
                IsActive = isActive,
            };

            // act & assert
            AssertValid(command);
        }
    }
}
