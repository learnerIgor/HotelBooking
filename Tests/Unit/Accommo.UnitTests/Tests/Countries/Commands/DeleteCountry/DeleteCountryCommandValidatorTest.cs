using Accommo.Application.Handlers.External.Locations.Countries.DeleteCountry;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Countries.Commands.DeleteCountry
{
    public class DeleteCountryCommandValidatorTest : ValidatorTestBase<DeleteCountryCommand>
    {
        public DeleteCountryCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteCountryCommand> TestValidator => TestFixture.Create<DeleteCountryCommandValidator>();

        [Theory]
        [FixtureInlineAutoData("dbace2b0-b73a-434a-b2fd-b61410a51c1e")]
        [FixtureInlineAutoData("809f5f23-4698-4807-8c85-258a641545b0")]
        [FixtureInlineAutoData("decd992b-bdc8-497d-88bb-ceb3d24228da")]
        public void Should_BeValid_When_CorrectCountryId(string countryId)
        {
            // arrange
            var idCountry = Guid.TryParse(countryId, out _);

            // act & assert
            Assert.True(idCountry);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("123425435")]
        [FixtureInlineAutoData("afsdfgsd23-2fsdfsdf")]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("    ")]
        public void Should_NotBeValid_When_IncorrectCountryId(string countryId)
        {
            // arrange
            var command = new DeleteCountryCommand
            {
                Id = countryId
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
