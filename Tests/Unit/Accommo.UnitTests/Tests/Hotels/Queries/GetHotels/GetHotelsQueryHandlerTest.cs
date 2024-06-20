using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Accommo.Application.Dtos;
using Accommo.Application.Handlers.Hotels.GetHotels;
using Moq;
using Accommo.Domain;
using Accommo.Application.Abstractions.Caches.Hotels;
using Accommo.Application.Dtos.Hotels;
using Accommo.Application.Exceptions;

namespace Accommo.UnitTests.Tests.Hotels.Queries.GetHotels;

public class GetHotelsQueryHandlerTest
{
    private readonly Mock<IBaseReadRepository<Hotel>> _hotelMock;
    private readonly Mock<IBaseReadRepository<Room>> _roomMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelListMemoryCache> _listMemoryCacheMock;
    private readonly GetHotelsQueryHandler _sut;

    public GetHotelsQueryHandlerTest()
    {
        _hotelMock = new Mock<IBaseReadRepository<Hotel>>();
        _roomMock = new Mock<IBaseReadRepository<Room>>();
        _mapperMock = new Mock<IMapper>();
        _listMemoryCacheMock = new Mock<IHotelListMemoryCache>();
        _sut = new GetHotelsQueryHandler(_hotelMock.Object, _roomMock.Object, _mapperMock.Object, _listMemoryCacheMock.Object);
    }

    [Fact]
    public async Task Should_BeValid_When_QueryCorrect()
    {
        // Arrange
        var query = new GetHotelsQuery
        {
            LocationText = "Italy",
            StartDate = "2024-07-01",
            EndDate = "2024-07-01",
            Offset = 0,
            Limit = 10
        };

        var hotels = new List<Hotel>
        {
            new(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg"),
            new(Guid.NewGuid(),"Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg")
        };

        var hotelDtos = new List<GetHotelDto>
        {
            new() { HotelId = Guid.Parse("67490f16-6c1a-40dd-b7ee-3b83004001d1") },
            new() { HotelId = Guid.Parse("67490f16-6c1a-40dd-b7ee-3b83004001d2") }
        };

        _hotelMock.Setup(r => r.AsQueryable())
            .Returns(hotels.AsQueryable());
        _hotelMock.Setup(r => r.AsAsyncRead().ToArrayAsync(It.IsAny<IQueryable<Hotel>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotels.ToArray());
        _hotelMock.Setup(r => r.AsAsyncRead().CountAsync(It.IsAny<IQueryable<Hotel>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotels.Count);
        _mapperMock.Setup(m => m.Map<GetHotelDto[]>(hotels))
            .Returns(hotelDtos.ToArray());

        // Act
        var result = await _sut.SentQueryAsync(query, CancellationToken.None);

        // Assert
        Assert.IsType<BaseListDto<GetHotelDto>>(result);
        Assert.Equal(hotels.Count, result.TotalCount);
        Assert.Equal(hotelDtos.Count, result.Items.Length);
    }

    [Fact]
    public async Task Should_BadOperationException_When_InccorrectDates()
    {
        // Arrange
        var query = new GetHotelsQuery
        {
            LocationText = "Italy",
            StartDate = "2024-06-01",
            EndDate = "2024-06-18",
            Offset = 0,
            Limit = 10
        };

        // Act and Assert
        var exception = await Assert.ThrowsAsync<BadOperationException>(() => _sut.SentQueryAsync(query, CancellationToken.None));
    }
}