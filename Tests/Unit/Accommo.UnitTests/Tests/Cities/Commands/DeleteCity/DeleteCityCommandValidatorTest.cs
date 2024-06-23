using Accommo.Application.Handlers.External.Locations.Cities.DeleteCity;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Cities.Commands.DeleteCity
{
    public class DeleteCityCommandValidatorTest : ValidatorTestBase<DeleteCityCommand>
    {
        public DeleteCityCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteCityCommand> TestValidator => TestFixture.Create<DeleteCityCommandValidator>();

        [Theory]
        [FixtureInlineAutoData("dbace2b0-b73a-434a-b2fd-b61410a51c1e")]
        [FixtureInlineAutoData("809f5f23-4698-4807-8c85-258a641545b0")]
        [FixtureInlineAutoData("decd992b-bdc8-497d-88bb-ceb3d24228da")]
        public void Should_BeValid_When_CorrectCityId(string cityId)
        {
            // arrange
            var idCity = Guid.TryParse(cityId, out _);

            // act & assert
            Assert.True(idCity);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("123425435")]
        [FixtureInlineAutoData("afsdfgsd23-2fsdfsdf")]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("    ")]
        public void Should_NotBeValid_When_IncorrectCityId(string cityId)
        {
            // arrange
            var command = new DeleteCityCommand
            {
                Id = cityId
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
