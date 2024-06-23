using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Moq;
using Accommo.Domain;
using Core.Tests;
using Xunit.Abstractions;
using Core.Tests.Fixtures;
using MediatR;
using System.Linq.Expressions;
using Accommo.Application.Handlers.Rooms.GetRoom;
using Accommo.Application.Dtos.Rooms;
using Accommo.Application.Abstractions.Caches.Rooms;

namespace Accommo.UnitTests.Tests.Rooms.Queries.GetRoom
{
    public class GetRoomQueryHandlerTest : RequestHandlerTestBase<GetRoomQuery, GetRoomDto>
    {
        private readonly Mock<IBaseReadRepository<Room>> _roomMock = new();
        private readonly Mock<IRoomMemoryCache> _mockHotelMemoryCache = new();
        private readonly IMapper _mapper;

        public GetRoomQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetRoomQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetRoomQuery, GetRoomDto> CommandHandler =>
            new GetRoomQueryHandler(_roomMock.Object, _mapper, _mockHotelMemoryCache.Object);

        [Fact]
        public async Task Should_BeValid_When_RoomFounded()
        {
            // arrange
            var hotelId = Guid.NewGuid();
            var query = new GetRoomQuery()
            {
                Id = hotelId.ToString()
            };

            var room = new Room(12, 12, Guid.NewGuid(), true, Guid.NewGuid(), [], "https://dfsdfsdfg");

            _roomMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(room);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_ThrowNotFound_When_RoomNotFound()
        {
            // arrange
            var query = new GetRoomQuery()
            {
                Id = Guid.NewGuid().ToString()
            };

            _roomMock
                .Setup(p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>(), default))
                .ReturnsAsync(null as Room);

            // act and assert
            await AssertThrowNotFound(query);
        }
    }
}