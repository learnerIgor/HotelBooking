using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Read;
using Core.Tests;
using Core.Tests.Fixtures;
using HR.Application.Handlers.Hotels;
using HR.Application.Handlers.Hotels.Queries.GetHotels;
using MediatR;
using Moq;
using HR.Application.Dtos;
using HR.Domain;
using Xunit.Abstractions;
using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Service;
using HR.Domain.Enums;

namespace HR.UnitTests.Tests.Hotels.Queries.GetHotels
{
    public class GetTodosQueryHandlerTest : RequestHandlerTestBase<GetHotelsQuery, BaseListDto<GetHotelDto>>
    {
        private readonly Mock<IBaseReadRepository<Hotel>> _hotelsMok = new();
        private readonly Mock<ICurrentUserService> _currentServiceMock = new();
        private readonly Mock<IHotelListMemoryCache >_hotelListMemoryCache = new();

        private readonly IMapper _mapper;

        public GetTodosQueryHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
        {
            _mapper = new AutoMapperFixture(typeof(GetHotelsQuery).Assembly).Mapper;
        }

        protected override IRequestHandler<GetHotelsQuery, BaseListDto<GetHotelDto>> CommandHandler =>
            new GetHotelsQueryHandler(_hotelsMok.Object, _mapper, _hotelListMemoryCache.Object);


        [Fact]
        public async Task Should_BeValid_When_GetTodosByAdmin()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetHotelsQuery();

            var todos = TestFixture.Build<Hotel>().CreateMany(10).ToArray();
            var count = todos.Length;

            _currentServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(true);

            _hotelsMok.Setup(
                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default)
            ).ReturnsAsync(todos);

            _hotelsMok.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }

        [Fact]
        public async Task Should_BeValid_When_GetTodosByClient()
        {
            // arrange
            var userId = Guid.NewGuid();
            _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);

            var query = new GetHotelsQuery();

            var todos = TestFixture.Build<Hotel>().CreateMany(10).ToArray();
            var count = todos.Length;

            _currentServiceMock.Setup(
                    p => p.UserInRole(ApplicationUserRolesEnum.Admin))
                .Returns(false);

            _hotelsMok.Setup(
                p => p.AsAsyncRead().ToArrayAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default)
            ).ReturnsAsync(todos);

            _hotelsMok.Setup(
                p => p.AsAsyncRead().CountAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default)
            ).ReturnsAsync(count);

            // act and assert
            await AssertNotThrow(query);
        }
    }
}