using Accommo.Application.Handlers.External.Bookings.Commands.DeleteBooking;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Bookings.Commands.DeleteBooking
{
    public class DeleteBookingCommandValidatorTest : ValidatorTestBase<DeleteBookingCommand>
    {
        public DeleteBookingCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteBookingCommand> TestValidator => TestFixture.Create<DeleteBookingCommandValidator>();

        [Theory]
        [FixtureInlineAutoData("dbace2b0-b73a-434a-b2fd-b61410a51c1e")]
        [FixtureInlineAutoData("809f5f23-4698-4807-8c85-258a641545b0")]
        [FixtureInlineAutoData("decd992b-bdc8-497d-88bb-ceb3d24228da")]
        public void Should_BeValid_When_CorrectBookingId(string bookingId)
        {
            // arrange
            var idBooking = Guid.TryParse(bookingId, out _);

            // act & assert
            Assert.True(idBooking);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("123425435")]
        [FixtureInlineAutoData("afsdfgsd23-2fsdfsdf")]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("    ")]
        public void Should_NotBeValid_When_IncorrectBookingId(string bookingId)
        {
            // arrange
            var command = new DeleteBookingCommand
            {
                Id = bookingId
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
