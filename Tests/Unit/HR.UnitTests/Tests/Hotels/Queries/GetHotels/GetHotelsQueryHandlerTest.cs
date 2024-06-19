using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.Dtos;
using HR.Application.Handlers.Hotels;
using HR.Application.Handlers.Hotels.Queries.GetHotels;
using Moq;
using HR.Domain;
using HR.Application.Abstractions.Caches.Hotels;

namespace HR.UnitTests.Tests.Hotels.Queries.GetHotels;

public class GetHotelsQueryHandlerTest
{
    private readonly Mock<IBaseReadRepository<Hotel>> _hotelRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IHotelListMemoryCache> _listMemoryCacheMock;
    private readonly GetHotelsQueryHandler _sut;

    public GetHotelsQueryHandlerTest()
    {
        _hotelRepositoryMock = new Mock<IBaseReadRepository<Hotel>>();
        _mapperMock = new Mock<IMapper>();
        _listMemoryCacheMock = new Mock<IHotelListMemoryCache>();
        _sut = new GetHotelsQueryHandler(_hotelRepositoryMock.Object, _mapperMock.Object, _listMemoryCacheMock.Object);
    }

    [Fact]
    public async Task SentQueryAsync_ShouldReturnBaseListDto()
    {
        // Arrange
        var query = new GetHotelsQuery
        {
            Offset = 0,
            Limit = 10
        };

        var hotels = new List<Hotel>
        {
            new Hotel("Hotel", Guid.NewGuid(), "description", "fsddfsdg", 4, true, "https://dfsdfsdfg"),
            new Hotel("Hotel", Guid.NewGuid(), "description", "fsddfsdg", 4, true, "https://dfsdfsdfg")
        };

        var hotelDtos = new List<GetHotelDto>
        {
            new GetHotelDto { HotelId = Guid.Parse("67490f16-6c1a-40dd-b7ee-3b83004001d1") },
            new GetHotelDto { HotelId = Guid.Parse("67490f16-6c1a-40dd-b7ee-3b83004001d2") }
        };

        _hotelRepositoryMock.Setup(r => r.AsQueryable())
            .Returns(hotels.AsQueryable());
        _hotelRepositoryMock.Setup(r => r.AsAsyncRead().ToArrayAsync(It.IsAny<IQueryable<Hotel>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(hotels.ToArray());
        _hotelRepositoryMock.Setup(r => r.AsAsyncRead().CountAsync(It.IsAny<IQueryable<Hotel>>(), It.IsAny<CancellationToken>()))
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
}