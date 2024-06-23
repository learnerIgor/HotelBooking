using Accommo.Application.Handlers.External.Locations.Countries.UpdateCountry;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Countries.Commands.UpdateCountry
{
    public class UpdateCountryCommandValidatorTest : ValidatorTestBase<UpdateCountryCommand>
    {
        public UpdateCountryCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateCountryCommand> TestValidator => TestFixture.Create<UpdateCountryCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var payLoad = new UpdateCountryCommandPayLoad { Name = "New name country" };

            var command = new UpdateCountryCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = payLoad.Name,
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
            var payLoad = new UpdateCountryCommandPayLoad { Name = name };

            var command = new UpdateCountryCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = payLoad.Name,
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
            var payLoad = new UpdateCountryCommandPayLoad { Name = name };

            var command = new UpdateCountryCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = payLoad.Name,
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
