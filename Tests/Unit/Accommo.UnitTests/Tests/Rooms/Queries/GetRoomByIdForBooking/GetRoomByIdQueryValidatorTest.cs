using Accommo.Application.Handlers.External.Rooms.GetRoomById;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Rooms.Queries.GetRoomByIdForBooking
{
    public class GetRoomByIdQueryValidatorTest : ValidatorTestBase<GetRoomByIdQuery>
    {
        public GetRoomByIdQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetRoomByIdQuery> TestValidator => TestFixture.Create<GetRoomByIdQueryValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_CorrectGuid(Guid id)
        {
            // arrange
            var query = new GetRoomByIdQuery
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
            var query = new GetRoomByIdQuery
            {
                Id = id
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
