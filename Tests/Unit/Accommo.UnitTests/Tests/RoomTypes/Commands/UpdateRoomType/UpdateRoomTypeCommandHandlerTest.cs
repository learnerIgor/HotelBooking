using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Write;
using Core.Tests;
using Core.Tests.Fixtures;
using MediatR;
using Moq;
using Accommo.Domain;
using Xunit.Abstractions;
using Accommo.Application.Caches;
using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Abstractions.Caches.Rooms;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;using Accommo.Application.Handlers.External.RoomTypes;
using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType;
using Azure.Core;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.UpdateRoomType;

public class UpdateRoomTypeCommandHandlerTest : RequestHandlerTestBase<UpdateRoomTypeCommand, GetRoomTypeExternalDto>
{
    private readonly Mock<IBaseWriteRepository<RoomType>> _roomTypeMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<UpdateRoomTypeCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public UpdateRoomTypeCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(UpdateRoomTypeCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<UpdateRoomTypeCommand, GetRoomTypeExternalDto> CommandHandler =>
        new UpdateRoomTypeCommandHandler(_roomTypeMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_UpdateRoomType_WhenRequestIsValid()
    {
        //Arrange
        var payLoad = new UpdateRoomTypePayload { Name = "Test RoomType" };

        var command = new UpdateRoomTypeCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = payLoad.Name
        };

        var roomType = new RoomType(Guid.Parse(command.Id), "NewRoomType", 1500.5M, true);
        roomType.UpdateName(command.Name);
        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(roomType);

        _cleanAccommoCacheService.ClearAllCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowNotFound_When_RoomTypeNotFound()
    {
        //Arrange
        var command = new UpdateRoomTypeCommand
        {
            Id = Guid.NewGuid().ToString(),
            Name = "Test RoomType"
        };

        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(null as RoomType);

        //Act and Assert
        await AssertThrowNotFound(command);
    }
}