using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomTypeCost;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.UpdateRoomTypeBaseCost
{
    public class UpdateRoomTypeBaseCostCommandValidatorTest : ValidatorTestBase<UpdateRoomTypeCostCommand>
    {
        public UpdateRoomTypeBaseCostCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<UpdateRoomTypeCostCommand> TestValidator => TestFixture.Create<UpdateRoomTypeCostCommandValidator>();

        [Fact]
        public void Should_BeValid_When_CommandIsValid()
        {
            // arrange
            var payLoad = new UpdateRoomTypeCostPayload { BaseCost = 1000.0M };

            var command = new UpdateRoomTypeCostCommand
            {
                Id = Guid.NewGuid().ToString(),
                BaseCost = payLoad.BaseCost
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData("dc7b523d-9642-4c27-b43e-a8cb6ac57e8d")]
        [FixtureInlineAutoData("423a2067-1f97-4a17-8b47-76a7b3784f1f")]
        [FixtureInlineAutoData("be67085b-e0df-4292-b812-ab783dbd9773")]
        public void Should_BeValid_When_CorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var idRoomType = Guid.TryParse(roomTypeId, out _);

            // act & assert
            Assert.True(idRoomType);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData("  ")]
        public void Should_NotBeValid_When_IncorrectRoomTypeId(string roomTypeId)
        {
            // arrange
            var idRoom = Guid.TryParse(roomTypeId, out _);

            // act & assert
            Assert.False(idRoom);
        }

        [Theory]
        [FixtureInlineAutoData(1345.5)]
        [FixtureInlineAutoData(3456)]
        [FixtureInlineAutoData(100.12)]
        public void Should_BeValid_When_BaseCostIsValid(decimal baseCost)
        {
            // arrange
            var payLoad = new UpdateRoomTypeCostPayload { BaseCost = baseCost };

            var command = new UpdateRoomTypeCostCommand
            {
                Id = Guid.NewGuid().ToString(),
                BaseCost = payLoad.BaseCost
            };

            // act & assert
            AssertValid(command);
        }

        [Theory]
        [FixtureInlineAutoData(0)]
        public void Should_NotBeValid_When_IncorrectBaseCost(decimal baseCost)
        {
            // arrange
            var payLoad = new UpdateRoomTypeCostPayload { BaseCost = baseCost };

            var command = new UpdateRoomTypeCostCommand
            {
                Id = Guid.NewGuid().ToString(),
                BaseCost = payLoad.BaseCost
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
