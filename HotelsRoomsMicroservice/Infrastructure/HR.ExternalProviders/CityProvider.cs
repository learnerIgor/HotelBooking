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
    public class CityProvider : ICityProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CityProvider(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        public async Task AddCityAsync(string token, string countryName, City city, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var postCityApiMethodUrl = $"{accommoServiceUrl}/Cities?countryName={countryName}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postCityApiMethodUrl);
            GetCity requestBody = new GetCity
            {
                CityId = city.CityId,
                Name = city.Name,
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{postCityApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task DeleteCityAsync(string token, Guid cityId, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var deleteCityApiMethodUrl = $"{accommoServiceUrl}/Cities/{cityId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, deleteCityApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{deleteCityApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateCityAsync(string token, string cityId, City city, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putCityApiMethodUrl = $"{accommoServiceUrl}/Cities/{cityId}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, putCityApiMethodUrl);
            GetCity requestBody = new GetCity
            {
                Name = city.Name,
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putCityApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
