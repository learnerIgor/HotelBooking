using Accommo.Application.Handlers.External.Locations.Countries.CreateCountry;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Countries.Commands.CreateCountry
{
    public class CreateCountryCommandValidatorTest : ValidatorTestBase<CreateCountryCommand>
    {
        public CreateCountryCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<CreateCountryCommand> TestValidator => TestFixture.Create<CreateCountryCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var command = new CreateCountryCommand
            {
                CountryId = Guid.NewGuid(),
                Name = "Name country",
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("dc7b523d-9642-4c27-b43e-a8cb6ac57e8d")]
        [FixtureInlineAutoData("423a2067-1f97-4a17-8b47-76a7b3784f1f")]
        [FixtureInlineAutoData("be67085b-e0df-4292-b812-ab783dbd9773")]
        public void Should_BeValid_When_CorrectCountryId(string countryId)
        {
            // arrange
            var idCountry = Guid.TryParse(countryId, out _);

            // act & assert
            Assert.True(idCountry);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectCountryId(string countryId)
        {
            // arrange
            var idCountry = Guid.TryParse(countryId, out _);

            // act & assert
            Assert.False(idCountry);
        }

        [Theory]
        [FixtureInlineAutoData("name")]
        [FixtureInlineAutoData("name new name")]
        [FixtureInlineAutoData("name1qw3ername1qw3ername1qw3ername1qw3ername1qw3er")]
        public void Should_BeValid_When_NameIsValid(string name)
        {
            // arrange
            var command = new CreateCountryCommand
            {
                CountryId = Guid.NewGuid(),
                Name = name,
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("  ")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("name1qw3ername1qw3ername1qw3ername1qw3ername1qw3er1")]
        public void Should_NotBeValid_When_IncorrectName(string name)
        {
            // arrange
            var command = new CreateCountryCommand
            {
                CountryId = Guid.NewGuid(),
                Name = name,
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
