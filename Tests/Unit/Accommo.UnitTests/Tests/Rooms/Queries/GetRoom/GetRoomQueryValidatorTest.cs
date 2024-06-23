using Accommo.Application.Handlers.Rooms.GetRoom;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Rooms.Queries.GetRoom
{
    public class GetRoomQueryValidatorTest : ValidatorTestBase<GetRoomQuery>
    {
        public GetRoomQueryValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<GetRoomQuery> TestValidator => TestFixture.Create<GetRoomQueryValidator>();

        [Theory, FixtureAutoData]
        public void Should_BeValid_When_CorrectGuid(Guid id)
        {
            // arrange
            var query = new GetRoomQuery
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
            var query = new GetRoomQuery
            {
                Id = id
            };

            // act & assert
            AssertNotValid(query);
        }
    }
}
