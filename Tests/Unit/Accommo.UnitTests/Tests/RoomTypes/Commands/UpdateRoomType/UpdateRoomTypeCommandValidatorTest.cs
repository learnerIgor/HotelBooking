using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.UpdateRoomType
{
    public class UpdateRoomTypeCommandValidatorTest : ValidatorTestBase<UpdateRoomTypeCommand>
    {
        public UpdateRoomTypeCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateRoomTypeCommand> TestValidator => TestFixture.Create<UpdateRoomTypeCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new UpdateRoomTypeCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Test",
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
            var command = new UpdateRoomTypeCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("name1qw3ername1qw3ername1qw3ername1qw3ername1qw3erasdasdas11")]
        public void Should_NotBeValid_When_IncorrectName(string name)
        {
            // arrange
            var command = new UpdateRoomTypeCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = name,
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
