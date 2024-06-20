using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Moq;
using Accommo.Domain;
using Accommo.Application.Abstractions.Caches.Hotels;
using Core.Tests;
using Xunit.Abstractions;
using Core.Tests.Fixtures;
using MediatR;
using System.Linq.Expressions;
using Accommo.Application.Handlers.Hotels.GetHotel;
using Accommo.Application.Dtos.Hotels;

namespace Accommo.UnitTests.Tests.Hotels.Queries.GetHotel
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

            var hotel = new Hotel(Guid.NewGuid(), "Hotel", Guid.NewGuid(), "description", 4, true, "https://dfsdfsdfg");

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