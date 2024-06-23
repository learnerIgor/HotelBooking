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
using System.Linq.Expressions;
using Accommo.Application.Handlers.External.RoomTypes;
using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomType;
using Accommo.Application.Handlers.External.RoomTypes.UpdateRoomTypeCost;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.UpdateRoomTypeBaseCost;

public class UpdateRoomTypeBaseCostCommandHandlerTest : RequestHandlerTestBase<UpdateRoomTypeCostCommand, GetRoomTypeExternalDto>
{
    private readonly Mock<IBaseWriteRepository<RoomType>> _roomTypeMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<UpdateRoomTypeCostCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public UpdateRoomTypeBaseCostCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(UpdateRoomTypeCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<UpdateRoomTypeCostCommand, GetRoomTypeExternalDto> CommandHandler =>
        new UpdateRoomTypeCostCommandHandler(_roomTypeMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_UpdateRoomTypeBaseCost_WhenRequestIsValid()
    {
        //Arrange
        var payLoad = new UpdateRoomTypeCostPayload { BaseCost = 1000.0M };

        var command = new UpdateRoomTypeCostCommand
        {
            Id = Guid.NewGuid().ToString(),
            BaseCost = payLoad.BaseCost
        };

        var roomType = new RoomType(Guid.Parse(command.Id), "NewRoomType", 1500.5M, true);
        roomType.UpdateBaseCost(command.BaseCost);
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
        var payLoad = new UpdateRoomTypeCostPayload { BaseCost = 1000.0M };

        var command = new UpdateRoomTypeCostCommand
        {
            Id = Guid.NewGuid().ToString(),
            BaseCost = payLoad.BaseCost
        };

        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(null as RoomType);

        //Act and Assert
        await AssertThrowNotFound(command);
    }
}