using Accommo.Application.Handlers.External.Bookings.Commands.CreateBooking;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidatorTest : ValidatorTestBase<CreateBookingCommand>
    {
        public CreateBookingCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateBookingCommand> TestValidator => TestFixture.Create<CreateBookingCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new CreateBookingCommand
            {
                ReservationId = Guid.NewGuid().ToString(),
                CheckInDate = "2024-07-01",
                CheckOutDate = "2024-07-10",
                IsActive = true,
                RoomId = Guid.NewGuid().ToString(),
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
        public void Should_BeValid_When_CorrectCheckInDate(string checkInDate)
        {
            // arrange
            var idCheckInDate = DateTime.TryParse(checkInDate, out _);

            // act & assert
            Assert.True(idCheckInDate);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectCheckInDate(string checkInDate)
        {
            // arrange
            var idCheckInDate = DateTime.TryParse(checkInDate, out _);

            // act & assert
            Assert.False(idCheckInDate);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-01")]
        [FixtureInlineAutoData("2025-05-01")]
        [FixtureInlineAutoData("2024-07-10")]
        public void Should_BeValid_When_CorrectCheckOutDate(string checkOutDate)
        {
            // arrange
            var idCheckOutDate = DateTime.TryParse(checkOutDate, out _);

            // act & assert
            Assert.True(idCheckOutDate);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectCheckOutDate(string checkOutDate)
        {
            // arrange
            var idCheckOutDate = DateTime.TryParse(checkOutDate, out _);

            // act & assert
            Assert.False(idCheckOutDate);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-01", "2024-07-10")]
        [FixtureInlineAutoData("2025-05-01", "2025-07-01")]
        [FixtureInlineAutoData("2024-07-10", "2024-07-11")]
        public void Should_BeValid_When_CheckInDateLessThanCheckOutDate(string checkInDate, string checkOutDate)
        {
            // arrange
            var idCheckOutDate = DateTime.Parse(checkOutDate);
            var idCheckInDate = DateTime.Parse(checkInDate);

            // act & assert
            Assert.True(idCheckInDate < idCheckOutDate);
        }

        [Theory]
        [FixtureInlineAutoData("2024-07-11", "2024-07-10")]
        [FixtureInlineAutoData("2025-07-10", "2025-07-01")]
        [FixtureInlineAutoData("2024-07-25", "2024-07-11")]
        public void Should_NotBeValid_When_CheckInDateMoreThanCheckOutDate(string checkInDate, string checkOutDate)
        {
            // arrange
            var idCheckOutDate = DateTime.Parse(checkOutDate);
            var idCheckInDate = DateTime.Parse(checkInDate);

            // act & assert
            Assert.False(idCheckInDate < idCheckOutDate);
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
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectRoomId(string roomId)
        {
            // arrange
            var idRoom = Guid.TryParse(roomId, out _);

            // act & assert
            Assert.False(idRoom);
        }
    }
}
