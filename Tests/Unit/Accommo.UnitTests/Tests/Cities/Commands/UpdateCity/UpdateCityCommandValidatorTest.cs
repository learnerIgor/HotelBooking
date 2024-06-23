using Accommo.Application.Handlers.External.Locations.Cities.UpdateCity;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Cities.Commands.UpdateCity
{
    public class UpdateCityCommandValidatorTest : ValidatorTestBase<UpdateCityCommand>
    {
        public UpdateCityCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateCityCommand> TestValidator => TestFixture.Create<UpdateCityCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var payLoad = new UpdateCityCommandPayLoad { Name = "New name city" };

            var command = new UpdateCityCommand
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
        public void Should_BeValid_When_CorrectCityId(string cityId)
        {
            // arrange
            var idCity = Guid.TryParse(cityId, out _);

            // act & assert
            Assert.True(idCity);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectCityId(string cityId)
        {
            // arrange
            var idCity = Guid.TryParse(cityId, out _);

            // act & assert
            Assert.False(idCity);
        }

        [Theory]
        [FixtureInlineAutoData("name")]
        [FixtureInlineAutoData("name new name")]
        [FixtureInlineAutoData("name1qw3ername1qw3ername1qw3ername1qw3ername1qw3er")]
        public void Should_BeValid_When_CityNameIsValid(string name)
        {
            // arrange
            var payLoad = new UpdateCityCommandPayLoad { Name = name };

            var command = new UpdateCityCommand
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
            var payLoad = new UpdateCityCommandPayLoad { Name = name };

            var command = new UpdateCityCommand
            {
                Id = Guid.NewGuid().ToString(),
                Name = payLoad.Name,
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
