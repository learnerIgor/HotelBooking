using AutoMapper;
using Accommo.Application.Abstractions.Persistence.Repositories.Read;
using Moq;
using Accommo.Domain;
using Core.Tests;
using Xunit.Abstractions;
using Core.Tests.Fixtures;
using MediatR;
using System.Linq.Expressions;
using Accommo.Application.Abstractions.Caches.Rooms;
using Accommo.Application.Handlers.External.Rooms.GetRoomById;
using Accommo.Application.Handlers.External.Rooms;

namespace Accommo.UnitTests.Tests.Rooms.Queries.GetRoomByIdForBooking
{
    public class GetRoomByIdQueryHandlerTest : RequestHandlerTestBase<GetRoomByIdQuery, GetRoomBookDto>
    {
        private readonly Mock<IBaseReadRepository<Room>> _roomMock = new();
        private readonly Mock<IRoomBookMemoryCache> _mockHotelMemoryCache = new();
        private readonly IMapper _mapper;

        public GetRoomByIdQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetRoomByIdQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetRoomByIdQuery, GetRoomBookDto> CommandHandler =>
            new GetRoomByIdQueryHandler(_roomMock.Object, _mapper, _mockHotelMemoryCache.Object);

        [Fact]
        public async Task Should_BeValid_When_RoomFounded()
        {
            // arrange
            var roomId = Guid.NewGuid();
            var query = new GetRoomByIdQuery()
            {
                Id = roomId.ToString()
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
            var query = new GetRoomByIdQuery()
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