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
using Accommo.Application.Handlers.External.RoomTypes.CreateRoomType;
using Accommo.Application.Handlers.External.RoomTypes;

namespace Accommo.UnitTests.Tests.RoomTypes.Commands.CreateRoomType;

public class CreateRoomTypeCommandHandlerTest : RequestHandlerTestBase<CreateRoomTypeCommand, GetRoomTypeExternalDto>
{
    private readonly Mock<IBaseWriteRepository<RoomType>> _roomTypeMock = new();
    private readonly ICleanAccommoCacheService _cleanAccommoCacheService;
    private readonly Mock<ILogger<CreateRoomTypeCommandHandler>> _loggerMock = new();
    private readonly IMapper _mapper;

    public CreateRoomTypeCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(CreateRoomTypeCommand).Assembly).Mapper;
        _cleanAccommoCacheService = new CleanAccommoCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object, new Mock<IRoomBookMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateRoomTypeCommand, GetRoomTypeExternalDto> CommandHandler =>
        new CreateRoomTypeCommandHandler(_roomTypeMock.Object, _mapper, _loggerMock.Object, _cleanAccommoCacheService);

    [Fact]
    public async Task Should_CreateRoomType_WhenRequestIsValid()
    {
        //Arrange
        var command = new CreateRoomTypeCommand
        {
            RoomTypeId = Guid.NewGuid(),
            Name = "Name",
            BaseCost = 100.34M,
            IsActive = true,
        };

        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(null as RoomType);

        var room = new RoomType(command.RoomTypeId, command.Name, command.BaseCost, command.IsActive);
        _roomTypeMock.Setup(p => p.AddAsync(room, default)).ReturnsAsync(room);

        _cleanAccommoCacheService.ClearListCaches();

        //Act and Assert
        await AssertNotThrow(command);
    }

    [Fact]
    public async Task Should_ThrowBadOperation_When_RoomTypeExist()
    {
        //Arrange
        var command = new CreateRoomTypeCommand
        {
            RoomTypeId = Guid.NewGuid(),
            Name = "Name",
            BaseCost = 100.34M,
            IsActive = true,
        };

        var roomType = new RoomType(command.RoomTypeId, command.Name, command.BaseCost, command.IsActive);
        _roomTypeMock
            .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<RoomType, bool>>>(), default))
            .ReturnsAsync(roomType);

        //Act and Assert
        await AssertThrowBadOperation(command, $"Type of room with name {command.Name} already exists in AccommoMicroservice.");
    }
}