using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using HR.Application.Handlers.Hotels.Queries.GetHotel;
using Xunit.Abstractions;

namespace HR.UnitTests.Tests.Hotels.Queries.GetHotel
{
    public class GetHotelQueryValidatorTest : ValidatorTestBase<GetHotelQuery>
    {
        public GetHotelQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetHotelQuery> TestValidator => TestFixture.Create<GetHotelQueryValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_CorrectGuid(Guid id)
        {
            // arrange
            var query = new GetHotelQuery
            {
                Id = id.ToString()
            };

            // act & assert
            AssertValid(query);
        }

        [Theory]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("123")]
        public void Should_NotBeValid_When_IncorrectGuid(string id)
        {
            // arrange
            var query = new GetHotelQuery
            {
                Id = id
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
