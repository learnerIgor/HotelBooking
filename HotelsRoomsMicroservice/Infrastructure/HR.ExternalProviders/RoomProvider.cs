using HR.Application.Abstractions.ExternalProviders;
using HR.Domain;
using HR.ExternalProviders.Exceptions;
using HR.ExternalProviders.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace HR.ExternalProviders
{
    public class RoomProvider : IRoomProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public RoomProvider(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        public async Task AddRoomAsync(string token, Room room, CancellationToken cancellationToken)
        {

            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var postRoomApiMethodUrl = $"{accommoServiceUrl}/Rooms";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postRoomApiMethodUrl);
            GetRoom requestBody = new GetRoom
            {
                RoomId = room.RoomId,
                Floor = room.Floor,
                Number = room.Number,
                IsActive = room.IsActive,
                Image = room.Image,
                RoomTypeId = room.RoomTypeId,
                HotelId = room.HotelId,
                Amenities = room.Amenities.Select(a => a.AmenityId).ToArray(),
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);

            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{postRoomApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateRoomAsync(string token, string roomId, Room room, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putRoomApiMethodUrl = $"{accommoServiceUrl}/Rooms/{roomId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, putRoomApiMethodUrl);
            GetRoom requestBody = new GetRoom
            {
                RoomId = room.RoomId,
                Floor = room.Floor,
                Number = room.Number,
                IsActive = room.IsActive,
                Image = room.Image,
                RoomTypeId = room.RoomTypeId,
                HotelId = room.HotelId,
                Amenities = room.Amenities.Select(a => a.AmenityId).ToArray(),
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putRoomApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task DeleteRoomAsync(string token, Guid roomId, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var deleteRoomApiMethodUrl = $"{accommoServiceUrl}/Rooms/{roomId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, deleteRoomApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{deleteRoomApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
