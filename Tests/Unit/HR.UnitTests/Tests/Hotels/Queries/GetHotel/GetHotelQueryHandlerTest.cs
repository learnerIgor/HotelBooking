using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using HR.Application.Handlers.Hotels;
using Moq;
using HR.Domain;
using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Handlers.Hotels.Queries.GetHotel;
using Core.Tests;
using Xunit.Abstractions;
using Core.Tests.Fixtures;
using MediatR;
using System.Linq.Expressions;

namespace HR.UnitTests.Tests.Hotels.Queries.GetHotel
{
    public class GetHotelQueryHandlerTest : RequestHandlerTestBase<GetHotelQuery, GetHotelDto>
    {
        private readonly Mock<IBaseReadRepository<Hotel>> _hotelsMock = new();
        private readonly Mock<IHotelMemoryCache> _mockHotelMemoryCache = new();
        private readonly IMapper _mapper;

        public GetHotelQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetHotelQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetHotelQuery, GetHotelDto> CommandHandler =>
            new GetHotelQueryHandler(_hotelsMock.Object, _mapper, _mockHotelMemoryCache.Object);

        [Fact]
        public async Task Should_BeValid_When_HotelFounded()
        {
            // arrange
            var hotelId = Guid.NewGuid();
            var query = new GetHotelQuery()
            {
                Id = hotelId.ToString()
            };

            var hotel = new Hotel("Hotel", Guid.NewGuid(), "description", "fsddfsdg", 4, true, "https://dfsdfsdfg");
            hotel.UpdateHotelId(hotelId);

            _hotelsMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(hotel);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_ThrowNotFound_When_HotelNotFound()
        {
            // arrange
            var query = new GetHotelQuery()
            {
                Id = Guid.NewGuid().ToString()
            };

            _hotelsMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default))
                .ReturnsAsync(null as Hotel);

            // act and assert
            await AssertThrowNotFound(query);
        }
    }
}