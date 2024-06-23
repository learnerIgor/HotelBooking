﻿using Accommo.Application.Handlers.External.Rooms.DeleteRoom;
using AutoFixture;
using Core.Tests;
using Core.Tests.Attributes;
using FluentValidation;
using Xunit.Abstractions;

namespace Accommo.UnitTests.Tests.Rooms.Commands.DeleteRoom
{
    public class DeleteRoomCommandValidatorTest : ValidatorTestBase<DeleteRoomCommand>
    {
        public DeleteRoomCommandValidatorTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
        }

        protected override IValidator<DeleteRoomCommand> TestValidator => TestFixture.Create<DeleteRoomCommandValidator>();

        [Theory]
        [FixtureInlineAutoData("dbace2b0-b73a-434a-b2fd-b61410a51c1e")]
        [FixtureInlineAutoData("809f5f23-4698-4807-8c85-258a641545b0")]
        [FixtureInlineAutoData("decd992b-bdc8-497d-88bb-ceb3d24228da")]
        public void Should_BeValid_When_CorrectHotelId(string hotelId)
        {
            // arrange
            var idHotel = Guid.TryParse(hotelId, out _);

            // act & assert
            Assert.True(idHotel);
        }

        [Theory]
        [FixtureInlineAutoData("sdfsdgfgdfg")]
        [FixtureInlineAutoData("123425435")]
        [FixtureInlineAutoData("afsdfgsd23-2fsdfsdf")]
        [FixtureInlineAutoData("")]
        [FixtureInlineAutoData(null)]
        [FixtureInlineAutoData("    ")]
        public void Should_NotBeValid_When_IncorrectRoomId(string roomId)
        {
            // arrange
            var command = new DeleteRoomCommand
            {
                Id = roomId
            };

            // act & assert
            AssertNotValid(command);
        }
    }
}
