using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.Dtos;
using Moq;
using Accommo.Domain;
using Accommo.Application.Exceptions;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Handlers.Rooms.GetRooms;
using Accommo.Application.Dtos.Rooms;

namespace Accommo.UnitTests.Tests.Rooms.Queries.GetRooms;

public class GetRoomsQueryHandlerTest
{
    private readonly Mock<IBaseReadRepository<Room>> _roomMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IRoomListMemoryCache> _listMemoryCacheMock;
    private readonly GetRoomsQueryHandler _sut;

    public GetRoomsQueryHandlerTest()
    {
        _roomMock = new Mock<IBaseReadRepository<Room>>();
        _mapperMock = new Mock<IMapper>();
        _listMemoryCacheMock = new Mock<IRoomListMemoryCache>();
        _sut = new GetRoomsQueryHandler(_roomMock.Object, _mapperMock.Object, _listMemoryCacheMock.Object);
    }

    [Fact]
    public async Task Should_BeValid_When_QueryCorrect()
    {
        // Arrange
        var query = new GetRoomsQuery
        {
            HotelId = Guid.NewGuid().ToString(),
            StartDate = "2024-07-01",
            EndDate = "2024-07-10",
            Offset = 0,
            Limit = 10
        };

        var rooms = new List<Room>
        {
            new(1,1,Guid.NewGuid(),true,Guid.NewGuid(),[],"https://dfsdfsdfg"),
            new(1,2,Guid.NewGuid(),true,Guid.NewGuid(),[],"https://dfsdfsdfg")
        };

        var roomDtos = new List<GetRoomDto>
        {
            new() { RoomId = Guid.Parse("67490f16-6c1a-40dd-b7ee-3b83004001d1") },
            new() { RoomId = Guid.Parse("67490f16-6c1a-40dd-b7ee-3b83004001d2") }
        };

        _roomMock.Setup(r => r.AsQueryable())
            .Returns(rooms.AsQueryable());
        _roomMock.Setup(r => r.AsAsyncRead().ToArrayAsync(It.IsAny<IQueryable<Room>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(rooms.ToArray());
        _roomMock.Setup(r => r.AsAsyncRead().CountAsync(It.IsAny<IQueryable<Room>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(rooms.Count);
        _mapperMock.Setup(m => m.Map<GetRoomDto[]>(rooms))
            .Returns(roomDtos.ToArray());

        // Act
        var result = await _sut.SentQueryAsync(query, CancellationToken.None);

        // Assert
        Assert.IsType<BaseListDto<GetRoomDto>>(result);
        Assert.Equal(rooms.Count, result.TotalCount);
        Assert.Equal(roomDtos.Count, result.Items.Length);
    }

    [Fact]
    public async Task Should_BadOperationException_When_InccorrectDates()
    {
        // Arrange
        var query = new GetRoomsQuery
        {
            HotelId = Guid.NewGuid().ToString(),
            StartDate = "2024-06-01",
            EndDate = "2024-06-18",
            Offset = 0,
            Limit = 10
        };

        // Act and Assert
        var exception = await Assert.ThrowsAsync<BadOperationException>(() => _sut.SentQueryAsync(query, CancellationToken.None));
    }
}