using Accommo.Application.Handlers.External.RoomTypes.DeleteRoomType;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.DeleteRoomType
{
    public class DeleteRoomTypeCommandValidatorTest : ValidatorTestBase<DeleteRoomTypeCommand>
    {
        public DeleteRoomTypeCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteRoomTypeCommand> TestValidator => TestFixture.Create<DeleteRoomTypeCommandValidator>();

        [Theory]
        [FixtureInlineAutoData("dbace2b0-b73a-434a-b2fd-b61410a51c1e")]
        [FixtureInlineAutoData("809f5f23-4698-4807-8c85-258a641545b0")]
        [FixtureInlineAutoData("decd992b-bdc8-497d-88bb-ceb3d24228da")]
        public void Should_BeValid_When_CorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var idHotel = Guid.TryParse(roomTypeId, out _);

            // act & assert
            Assert.True(idHotel);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("123425435")]
        [FixtureInlineAutoData("afsdfgsd23-2fsdfsdf")]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("    ")]
        public void Should_NotBeValid_When_IncorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var command = new DeleteRoomTypeCommand
            {
                Id = roomTypeId
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
