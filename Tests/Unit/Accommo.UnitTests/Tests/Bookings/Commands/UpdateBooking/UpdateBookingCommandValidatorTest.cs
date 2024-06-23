using Accommo.Application.Handlers.External.Bookings.Commands.UpdateBooking;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Bookings.Commands.UpdateBooking
{
    public class UpdateBookingCommandValidatorTest : ValidatorTestBase<UpdateBookingCommand>
    {
        public UpdateBookingCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateBookingCommand> TestValidator => TestFixture.Create<UpdateBookingCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new UpdateBookingCommand
            {
                ReservationId = Guid.NewGuid().ToString(),
                StartDate = "2024-07-01",
                EndDate = "2024-07-10",
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("dc7b523d-9642-4c27-b43e-a8cb6ac57e8d")]
        [FixtureInlineAutoData("423a2067-1f97-4a17-8b47-76a7b3784f1f")]
        [FixtureInlineAutoData("be67085b-e0df-4292-b812-ab783dbd9773")]
        public void Should_BeValid_When_CorrectReservationId(string reservationId)
        {
            // arrange
            var idReservation = Guid.TryParse(reservationId, out _);

            // act & assert
            Assert.True(idReservation);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectReservationId(string reservationId)
        {
            // arrange
            var idReservation = Guid.TryParse(reservationId, out _);

            // act & assert
            Assert.False(idReservation);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-01")]
        [FixtureInlineAutoData("2025-05-01")]
        [FixtureInlineAutoData("2024-07-10")]
        public void Should_BeValid_When_CorrectStartDate(string startDate)
        {
            // arrange
            var date = DateTime.TryParse(startDate, out _);

            // act & assert
            Assert.True(date);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectStartDate(string startDate)
        {
            // arrange
            var date = DateTime.TryParse(startDate, out _);

            // act & assert
            Assert.False(date);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-01")]
        [FixtureInlineAutoData("2025-05-01")]
        [FixtureInlineAutoData("2024-07-10")]
        public void Should_BeValid_When_CorrectEndDate(string endDate)
        {
            // arrange
            var date = DateTime.TryParse(endDate, out _);

            // act & assert
            Assert.True(date);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectEndDate(string endDate)
        {
            // arrange
            var date = DateTime.TryParse(endDate, out _);

            // act & assert
            Assert.False(date);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-01", "2024-07-10")]
        [FixtureInlineAutoData("2025-05-01", "2025-07-01")]
        [FixtureInlineAutoData("2024-07-10", "2024-07-11")]
        public void Should_BeValid_When_StartDateLessThanEndDate(string startDate, string endDate)
        {
            // arrange
            var dateIn = DateTime.Parse(startDate);
            var dateOut = DateTime.Parse(endDate);

            // act & assert
            Assert.True(dateIn < dateOut);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-11", "2024-07-10")]
        [FixtureInlineAutoData("2025-07-10", "2025-07-01")]
        [FixtureInlineAutoData("2024-07-25", "2024-07-11")]
        public void Should_NotBeValid_When_StartDateMoreThanEndDate(string startDate, string endDate)
        {
            // arrange
            var dateIn = DateTime.Parse(startDate);
            var dateOut = DateTime.Parse(endDate);

            // act & assert
            Assert.False(dateIn < dateOut);
        }
    }
}
