using Core.Tests;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net;
using System.Text.Json;
using Core.Tests.Attributes;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using Moq;
using Accommo.Application.Handlers.Hotels.GetHotel;
using Accommo.Application.Dtos.Hotels;

namespace Accommo.FunctionalTests
{
    public class AccommoApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private const string adminToken =
            "eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiQWRtaW4iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjM1ZjM3MzQwLWY5ZTUtNDExOC1iOTQ5LTA4ZGM1MWNjNTdiNyIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkFkbWluIiwiZXhwIjoxNzE5MTYwOTg5LCJpc3MiOiJIb3RlbEJvb2siLCJhdWQiOiJIb3RlbEJvb2sifQ.qQ94_oopZ67cp2jft3liwG7kT06xmaETCCJlLsLMxsg";
        public AccommoApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [FixtureInlineAutoData("35F37340-F9E5-4118-B949-08DC51CC57B7")]
        [FixtureInlineAutoData("35F37340-F9E5-4118-B949-08DC51CC57B8")]
        public async Task Get_HotelById_ReturnSuccessAndCorrectContentType(string id)
        {
            // Arrange
            var client = _factory.CreateClient();

            var query = new GetHotelQuery()
            {
                Id = id
            };

            // Act
            using (var requestMessage =
                   new HttpRequestMessage(HttpMethod.Get, $"/Hotels/{query.Id}"))
            {
                var response = await client.SendAsync(requestMessage);

                var responseJson = await response.Content.ReadAsStringAsync();
                var responseObject = JsonSerializer.Deserialize<GetHotelDto>(responseJson,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Assert
                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
                Assert.Equal(responseObject!.HotelId, Guid.Parse(query.Id));
                Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
            }
        }

        //[Theory]
        //[FixtureInlineAutoData(null, null)]
        //[FixtureInlineAutoData(null, 1)]
        //[FixtureInlineAutoData(1, null)]
        //[FixtureInlineAutoData(1, 1)]
        //public async Task Get_Hotels_ReturnSuccessAndCorrectContentType(int? limit, int? offset)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new Dictionary<string, string?>
        //    {
        //        ["limit"] = limit?.ToString(),
        //        ["offset"] = offset?.ToString(),
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/Hotels", query)))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        // Assert
        //        response.EnsureSuccessStatusCode(); // Status Code 200-299
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Fact]
        //public async Task Create_Hotel_ReturnCreatedAndCorrectContentType()
        //{
        //    // Arrange
        //    var command = new CreateHotelCommand
        //    {
        //        Name = "New Hotel",
        //        Description = "Create a new Hotel",
        //        IBAN = "IT60X0542811101000000123456",
        //        Rating = 5,
        //        Image = "https://image",
        //        Address = new AddressDto
        //        {
        //            Street = "Test Street",
        //            HouseNumber = "123",
        //            Latitude = 45.45M,
        //            Longitude = 45.45M,
        //            City = new CityDto
        //            {
        //                Name = "ашь",
        //                Country = new CountryDto { Name = "Шђрышџ" }
        //            }
        //        }
        //    };

        //    var client = _factory.CreateClient();

        //    // Act
        //    using (var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/Hotel"))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        requestMessage.Content =
        //            new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

        //        var mockHotelRoomProvider = new Mock<IHotelProvider>();
        //        mockHotelRoomProvider.Setup(x => x.AddHotelAsync(It.IsAny<string>(), It.IsAny<Hotel>(), It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        //        var response = await client.SendAsync(requestMessage);

        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var responseObject = JsonSerializer.Deserialize<GetHotelDto>(responseJson,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        // Assert
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //        Assert.Equal(responseObject!.Name, command.Name);
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData("12884305-a62a-4ac5-87e1-7f5e8630bd3b")]
        //[FixtureInlineAutoData("e2ed8552-529a-49c0-9d1d-ad0e3b9a456f")]
        //[FixtureInlineAutoData("72d878a3-4819-48b3-b83e-03d255bf6c9b")]
        //public async Task Get_CityById_ReturnSuccessAndCorrectContentType(string id)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new GetCityQuery()
        //    {
        //        Id = id
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, $"/City/{query.Id}"))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var responseObject = JsonSerializer.Deserialize<GetCityDto>(responseJson,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        // Assert
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //        Assert.Equal(responseObject!.CityId, Guid.Parse(query.Id));
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData(null, null)]
        //[FixtureInlineAutoData(null, 1)]
        //[FixtureInlineAutoData(1, null)]
        //[FixtureInlineAutoData(1, 1)]
        //public async Task Get_Cities_ReturnSuccessAndCorrectContentType(int? limit, int? offset)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new Dictionary<string, string?>
        //    {
        //        ["limit"] = limit?.ToString(),
        //        ["offset"] = offset?.ToString(),
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/Cities", query)))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        // Assert
        //        response.EnsureSuccessStatusCode(); // Status Code 200-299
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData("bef28182-c76f-4467-a393-b39790b2cd0a")]
        //[FixtureInlineAutoData("aef107bc-e839-4ca7-9fdf-31580887d2c8")]
        //[FixtureInlineAutoData("7ee80d45-33f6-4ed5-a729-55d0372a3075")]
        //public async Task Get_CountryById_ReturnSuccessAndCorrectContentType(string id)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new GetCountryQuery()
        //    {
        //        Id = id
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, $"/Country/{query.Id}"))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var responseObject = JsonSerializer.Deserialize<GetCountryDto>(responseJson,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        // Assert
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //        Assert.Equal(responseObject!.CountryId, Guid.Parse(query.Id));
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData(null, null)]
        //[FixtureInlineAutoData(null, 1)]
        //[FixtureInlineAutoData(1, null)]
        //[FixtureInlineAutoData(1, 1)]
        //public async Task Get_Countries_ReturnSuccessAndCorrectContentType(int? limit, int? offset)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new Dictionary<string, string?>
        //    {
        //        ["limit"] = limit?.ToString(),
        //        ["offset"] = offset?.ToString(),
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString("/Countries", query)))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        // Assert
        //        response.EnsureSuccessStatusCode(); // Status Code 200-299
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b1")]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b2")]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b3")]
        //public async Task Get_RoomById_ReturnSuccessAndCorrectContentType(string id)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new GetRoomQuery()
        //    {
        //        Id = id
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, $"/Room/{query.Id}"))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var responseObject = JsonSerializer.Deserialize<GetRoomDto>(responseJson,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        // Assert
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //        Assert.Equal(responseObject!.RoomId, Guid.Parse(query.Id));
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b7", null, null)]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b8", null, 1)]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b7", 1, null)]
        //[FixtureInlineAutoData("35f37340-f9e5-4118-b949-08dc51cc57b8", 1, 1)]
        //public async Task Get_Rooms_ReturnSuccessAndCorrectContentType(string hotelId, int? limit, int? offset)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new Dictionary<string, string?>
        //    {
        //        ["hotelId"] = hotelId,
        //        ["limit"] = limit?.ToString(),
        //        ["offset"] = offset?.ToString(),
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString($"/Rooms", query)))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        // Assert
        //        response.EnsureSuccessStatusCode(); // Status Code 200-299
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData("24255a44-d32f-47bf-8deb-4ac3e69adf3a")]
        //[FixtureInlineAutoData("ca7083ba-52f1-4c83-bcbd-8232ebb139ab")]
        //[FixtureInlineAutoData("8c6d84dc-01ef-4451-89cb-d8af4af7be2c")]
        //public async Task Get_RoomTypeById_ReturnSuccessAndCorrectContentType(string id)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new GetRoomTypeQuery()
        //    {
        //        Id = id
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, $"/RoomType/{query.Id}"))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        var responseJson = await response.Content.ReadAsStringAsync();
        //        var responseObject = JsonSerializer.Deserialize<GetRoomTypeDto>(responseJson,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        // Assert
        //        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //        Assert.Equal(responseObject!.RoomTypeId, Guid.Parse(query.Id));
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}

        //[Theory]
        //[FixtureInlineAutoData(null, null)]
        //[FixtureInlineAutoData(null, 1)]
        //[FixtureInlineAutoData(1, null)]
        //[FixtureInlineAutoData(1, 1)]
        //public async Task Get_RoomTypes_ReturnSuccessAndCorrectContentType(string hotelId, int? limit, int? offset)
        //{
        //    // Arrange
        //    var client = _factory.CreateClient();

        //    var query = new Dictionary<string, string?>
        //    {
        //        ["hotelId"] = hotelId,
        //        ["limit"] = limit?.ToString(),
        //        ["offset"] = offset?.ToString(),
        //    };

        //    // Act
        //    using (var requestMessage =
        //           new HttpRequestMessage(HttpMethod.Get, QueryHelpers.AddQueryString($"/RoomTypes", query)))
        //    {
        //        requestMessage.Headers.Authorization =
        //            new AuthenticationHeaderValue("Bearer", adminToken);

        //        var response = await client.SendAsync(requestMessage);

        //        // Assert
        //        response.EnsureSuccessStatusCode(); // Status Code 200-299
        //        Assert.Equal("application/json; charset=utf-8",
        //            response.Content.Headers.ContentType.ToString());
        //    }
        //}
    }
}