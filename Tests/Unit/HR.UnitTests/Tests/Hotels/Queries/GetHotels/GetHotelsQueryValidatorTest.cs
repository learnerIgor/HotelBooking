using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using HR.Application.Handlers.Hotels.Queries.GetHotels;
using Xunit.Abstractions;

namespace HR.UnitTests.Tests.Hotels.Queries.GetHotels
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
                FreeText = "Hotel",
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData(1)]
        [FixtureInlineAutoData(10)]
        [FixtureInlineAutoData(20)]
        public void Should_BeValid_When_LimitIsValid(int limit)
        {
            // arrange
            var query = new GetHotelsQuery
            {
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
                Limit = limit,
            };

            // act & assert
            AssertNotValid(query);
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
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("12345")]
        [FixtureInlineAutoData("1#*&^%$#@#$%±~`}{][\\|?/.,<>")]
        [FixtureInlineAutoData("12345678901234567890123456789012345678901234567890")]
        public void Should_BeValid_When_FreeTextIsValid(string freeText)
        {
            // arrange
            var query = new GetHotelsQuery
            {
                FreeText = freeText,
            };

            // act & assert
            AssertValid(query);
        }

        [Fact]
        public void Should_NotBeValid_When_IncorrectFreeText()
        {
            // arrange
            var query = new GetHotelsQuery
            {
                FreeText = "1234567890qwertyuiopasdfghjklzxcvbnm,./';76[p=-09812",
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
