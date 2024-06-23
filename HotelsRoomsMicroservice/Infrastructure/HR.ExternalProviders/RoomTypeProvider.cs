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
    public class RoomTypeProvider : IRoomTypeProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public RoomTypeProvider(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        public async Task AddRoomTypeAsync(string token, RoomType roomType, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var postRoomTypeApiMethodUrl = $"{accommoServiceUrl}/RoomTypes";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postRoomTypeApiMethodUrl);
            GetRoomType requestBody = new GetRoomType
            {
                RoomTypeId = roomType.RoomTypeId,
                Name = roomType.Name,
                BaseCost = roomType.BaseCost,
                IsActive = roomType.IsActive,
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);

            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{postRoomTypeApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task DeleteRoomTypeAsync(string token, Guid roomTypeId, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var deleteRoomTypeApiMethodUrl = $"{accommoServiceUrl}/RoomTypes/{roomTypeId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, deleteRoomTypeApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{deleteRoomTypeApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateCostRoomTypeAsync(string token, string roomTypeId, RoomType roomType, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putRoomTypeApiMethodUrl = $"{accommoServiceUrl}/RoomTypes/{roomTypeId}/Cost";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, putRoomTypeApiMethodUrl);
            GetRoomType requestBody = new GetRoomType
            {
                BaseCost = roomType.BaseCost
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putRoomTypeApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateRoomTypeAsync(string token, string roomTypeId, RoomType roomType, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putRoomTypeApiMethodUrl = $"{accommoServiceUrl}/RoomTypes/{roomTypeId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, putRoomTypeApiMethodUrl);
            GetRoomType requestBody = new GetRoomType
            {
                Name = roomType.Name
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putRoomTypeApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
