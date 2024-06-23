using Accommo.Application.Handlers.Rooms.GetRooms;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Rooms.Queries.GetRooms
{
    public class GetRoomsQueryValidatorTest : ValidatorTestBase<GetRoomsQuery>
    {
        public GetRoomsQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetRoomsQuery> TestValidator => TestFixture.Create<GetRoomsQueryValidator>();

        [Fact]
        public void Should_BeValid_When_RequestIsValid()
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = Guid.NewGuid().ToString(),
                Limit = 2,
                Offset = 4,
                StartDate = "2024-07-01",
                EndDate = "2024-07-10",
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(19)]
        public void Should_BeValid_When_LimitIsValid(int limit)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = Guid.NewGuid().ToString(),
                StartDate = "2024-07-01",
                EndDate = "2024-07-10",
                Limit = limit,
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(-5)]
        [FixtureInlineAutoData(0)]
        [FixtureInlineAutoData(30)]
        public void Should_NotBeValid_When_IncorrectLimit(int limit)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                Limit = limit,
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(19)]
        public void Should_BeValid_When_OffsetIsValid(int offset)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = Guid.NewGuid().ToString(),
                StartDate = "2024-07-01",
                EndDate = "2024-07-10",
                Offset = offset,
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(-5)]
        [FixtureInlineAutoData(0)]
        public void Should_NotBeValid_When_IncorrectOffset(int offset)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                Offset = offset,
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("08f22129-6937-406d-801a-340127a28023")]
        [FixtureInlineAutoData("b5d1350d-f5b0-4a13-ad23-dc94bae3996b")]
        [FixtureInlineAutoData("67ae9563-74c0-4b4b-bbb4-d7fc435d917c")]
        public void Should_BeValid_When_HotelIdIsValid(string hotelId)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = hotelId,
                StartDate = "2024-07-01",
                EndDate = "2024-07-10"
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("1234567890qwertyuiopasdfghjklzxcvbnm,./';76[p=-09812")]
        public void Should_NotBeValid_When_IncorrectHotelId(string hotelId)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = hotelId,
                StartDate = "2024-07-01",
                EndDate = "2024-07-10"
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("2024-10-12", "2024-10-20")]
        [FixtureInlineAutoData("2024-07-01", "2024-07-10")]
        public void Should_BeValid_When_CorrectDates(string startDate, string endDate)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = Guid.NewGuid().ToString(),
                StartDate = startDate,
                EndDate = endDate
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("", "")]
        [FixtureInlineAutoData(null, null)]
        [FixtureInlineAutoData("1234567890qwertyui", "dsadasdfsgdfsdf")]
        [FixtureInlineAutoData("2024-07-01", "2024-07-01")]
        [FixtureInlineAutoData("2024-07-01", "2024-06-01")]
        public void Should_NotBeValid_When_IncorrectDates(string startDate, string endDate)
        {
            // arrange
            var query = new GetRoomsQuery
            {
                HotelId = Guid.NewGuid().ToString(),
                StartDate = startDate,
                EndDate = endDate
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
