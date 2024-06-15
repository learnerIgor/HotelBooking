using System.Linq.Expressions;
using AutoFixture;
using AutoMapper;
using HR.Application.Abstractions.Persistence.Repositories.Write;
using Core.Tests.Fixtures;
using Core.Tests.Helpers;
using MediatR;
using Moq;
using HR.Application.Caches;
using HR.Application.Handlers.Hotels.Commands.CreateHotel;
using HR.Application.Handlers.Hotels.Queries.GetHotels;
using HR.Domain;
using Xunit.Abstractions;
using Core.Tests;
using HR.Application.Handlers.Hotels;
using HR.Application.Abstractions.Service;
using HR.Application.Abstractions.Caches.Hotels;
using HR.Application.Abstractions.Caches.Rooms;
using HR.Application.Abstractions.Caches.RoomTypes;
using HR.Application.Abstractions.Caches.Locations.Cities;
using HR.Application.Abstractions.Caches.Locations.Countries;
using HR.Domain.Enums;
using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using HR.Application.Abstractions.ExternalProviders;

namespace HR.UnitTests.Tests.Hotels.Commands.CreateHotel;

public class CreateHotelCommandHandlerTest : RequestHandlerTestBase<CreateHotelCommand, GetHotelDto>
{
    private readonly Mock<IBaseWriteRepository<Hotel>> _hotelsMock = new();
    private readonly Mock<IBaseWriteRepository<Country>> _countryMock = new();
    private readonly Mock<IBaseWriteRepository<City>> _cityMock = new();
    private readonly Mock<IBaseWriteRepository<Address>> _addressMock = new();
    private readonly Mock<ILogger<CreateHotelCommandHandler>> _loggerMok = new();
    private readonly Mock<ICurrentUserService> _currentServiceMock = new();
    private readonly Mock<IHotelProvider> _hotelProvider = new();
    private readonly ICleanHotelRoomCacheService _cleanHotelRoomCacheService;
    private readonly IMapper _mapper;

    public CreateHotelCommandHandlerTest(ITestOutputHelper testOutputHelper) : base(testOutputHelper)
    {
        _mapper = new AutoMapperFixture(typeof(GetHotelsQuery).Assembly).Mapper;
        _cleanHotelRoomCacheService = new CleanHotelRoomCacheService(new Mock<IHotelMemoryCache>().Object, new Mock<IHotelListMemoryCache>().Object, new Mock<IRoomMemoryCache>().Object, new Mock<IRoomListMemoryCache>().Object,
            new Mock<IRoomTypeMemoryCache>().Object, new Mock<IRoomTypeListMemoryCache>().Object, new Mock<ICityListMemoryCache>().Object, new Mock<ICityMemoryCache>().Object, new Mock<ICountryMemoryCache>().Object, new Mock<ICountryListMemoryCache>().Object);
    }

    protected override IRequestHandler<CreateHotelCommand, GetHotelDto> CommandHandler =>
        new CreateHotelCommandHandler(_hotelsMock.Object, _countryMock.Object, _cityMock.Object, _addressMock.Object, _mapper, _loggerMok.Object, _cleanHotelRoomCacheService, _hotelProvider.Object, _currentServiceMock.Object);

    //[Theory]
    //public async Task Should_BeValid_When_GetHotelsByAdmin(CreateHotelCommand command, Guid userId)
    //{
    //    // arrange
    //    _currentServiceMock.SetupGet(p => p.CurrentUserId).Returns(userId);
    //    _currentServiceMock.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Admin)).Returns(true);

    //    var hotel = TestFixture.Build<Hotel>().Create();
    //    hotel.HotelId = GuidHelper.GetNotEqualGiud(userId);
    //    _todosMok.Setup(
    //        p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Hotel, bool>>>(), default)
    //    ).ReturnsAsync(hotel);

    //    // act and assert
    //    await AssertNotThrow(command);
    //}

    //[Theory, FixtureInlineAutoData]
    //public async Task Should_BeValid_When_GetTodosByClient(UpdateTodoCommand command, Guid userId)
    //{
    //    // arrange
    //    _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
    //    _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

    //    var todo = TestFixture.Build<Todo>().Create();
    //    todo.OwnerId = userId;
    //    _todosMok.Setup(
    //        p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Todo, bool>>>(), default)
    //    ).ReturnsAsync(todo);

    //    // act and assert
    //    await AssertNotThrow(command);
    //}

    //[Theory, FixtureInlineAutoData]
    //public async Task Should_ThrowForbidden_When_GetTodosWithOtherOwnerByClient(UpdateTodoCommand command, Guid userId)
    //{
    //    // arrange
    //    _currentServiceMok.SetupGet(p => p.CurrentUserId).Returns(userId);
    //    _currentServiceMok.Setup(p => p.UserInRole(ApplicationUserRolesEnum.Client)).Returns(true);

    //    var todo = TestFixture.Build<Todo>().Create();
    //    todo.OwnerId = GuidHelper.GetNotEqualGiud(userId);

    //    _todosMok.Setup(
    //        p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Todo, bool>>>(), default)
    //    ).ReturnsAsync(todo);

    //    // act and assert
    //    await AssertThrowForbiddenFound(command);
    //}

    //[Theory, FixtureInlineAutoData]
    //public async Task Should_ThrowNotFound_When_TodoNotFound(UpdateTodoCommand command)
    //{
    //    // arrange

    //    _todosMok.Setup(
    //        p => p.AsAsyncRead().SingleOrDefaultAsync(It.IsAny<Expression<Func<Todo, bool>>>(), default)
    //    ).ReturnsAsync(null as Todo);

    //    // act and assert
    //    await AssertThrowNotFound(command);
    //}
}