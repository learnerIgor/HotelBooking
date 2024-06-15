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
    public class CountryProvider : ICountryProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public CountryProvider(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        public async Task AddCountryAsync(string token, Country country, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var postCountryApiMethodUrl = $"{accommoServiceUrl}/Country";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postCountryApiMethodUrl);
            GetCountry requestBody = new GetCountry
            {
                CountryId = country.CountryId,
                Name = country.Name,
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);

            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{postCountryApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task DeleteCountryAsync(string token, Guid countryId, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var deleteCountryApiMethodUrl = $"{accommoServiceUrl}/Country/{countryId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, deleteCountryApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{deleteCountryApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateCountryAsync(string token, string countryId, Country country, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putCountryApiMethodUrl = $"{accommoServiceUrl}/Country/{countryId}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, putCountryApiMethodUrl);
            GetCountry requestBody = new GetCountry
            {
                Name = country.Name,
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putCountryApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
