using Accommo.Application.Handlers.Hotels.GetHotels;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Hotels.Queries.GetHotels
{
    public class GetHotelsQueryValidatorTest : ValidatorTestBase<GetHotelsQuery>
    {
        public GetHotelsQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetHotelsQuery> TestValidator => TestFixture.Create<GetHotelsQueryValidator>();

        [Fact]
        public void Should_BeValid_When_RequestIsValid()
        {
            // arrange
            var query = new GetHotelsQuery
            {
                Limit = 2,
                Offset = 4,
                StartDate = "2024-07-01",
                EndDate = "2024-07-10",
                LocationText = "Italy",
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
            var query = new GetHotelsQuery
            {
                LocationText = "Italy",
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
            var query = new GetHotelsQuery
            {
                LocationText = "Italy",
                StartDate = "2024-07-01",
                EndDate = "2024-07-10",
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
            var query = new GetHotelsQuery
            {
                LocationText = "Italy",
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
            var query = new GetHotelsQuery
            {
                Offset = offset,
            };

            // act & assert
            AssertNotValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("12345")]
        [FixtureInlineAutoData("1#*&^%$#@#$%±~`}{][\\|?/.,<>")]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890")]
        public void Should_BeValid_When_FreeTextIsValid(string freeText)
        {
            // arrange
            var query = new GetHotelsQuery
            {
                LocationText = freeText,
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
        public void Should_NotBeValid_When_IncorrectFreeText(string freeText)
        {
            // arrange
            var query = new GetHotelsQuery
            {
                LocationText = freeText,
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
            var query = new GetHotelsQuery
            {
                LocationText = "Italy",
                StartDate = startDate,
                EndDate = endDate
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("","")]
        [FixtureInlineAutoData(null,null)]
        [FixtureInlineAutoData("1234567890qwertyui","dsadasdfsgdfsdf")]
        [FixtureInlineAutoData("2024-07-01", "2024-07-01")]
        [FixtureInlineAutoData("2024-07-01", "2024-06-01")]
        public void Should_NotBeValid_When_IncorrectDates(string startDate, string endDate)
        {
            // arrange
            var query = new GetHotelsQuery
            {
                LocationText = "Italy",
                StartDate = startDate,
                EndDate = endDate
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
