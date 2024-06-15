using HR.ExternalProviders.Exceptions;
using Microsoft.Extensions.Configuration;
using System.Net;
using HR.Application.Abstractions.ExternalProviders;
using HR.Domain;
using HR.ExternalProviders.Models;
using System.Text;
using Newtonsoft.Json;

namespace HR.ExternalProviders
{
    public class HotelProvider : IHotelProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public HotelProvider(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }

        public async Task AddHotelAsync(string token, Hotel hotel, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var postHotelApiMethodUrl = $"{accommoServiceUrl}/Hotel";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postHotelApiMethodUrl);
            GetHotel requestBody = new GetHotel
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Description = hotel.Description,
                IBAN = hotel.IBAN,
                Rating = hotel.Rating,
                IsActive = hotel.IsActive,
                Image = hotel.Image,
                AddressId = hotel.AddressId,
                Address = new GetAddress
                {
                    Street = hotel.Address.Street,
                    HouseNumber = hotel.Address.HouseNumber,
                    Latitude = hotel.Address.Latitude,
                    Longitude = hotel.Address.Longitude,
                    IsActive = hotel.Address.IsActive,
                    City = new GetCity
                    {
                        CityId = hotel.Address.City.CityId,
                        Name = hotel.Address.City.Name,
                        IsActive = hotel.IsActive,
                        Country = new GetCountry
                        {
                            CountryId = hotel.Address.City.Country.CountryId,
                            Name = hotel.Address.City.Country.Name,
                            IsActive= hotel.Address.City.Country.IsActive
                        }
                    }
                }
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{postHotelApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateHotelAsync(string token, string hotelId, Hotel hotel, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putHotelApiMethodUrl = $"{accommoServiceUrl}/Hotel/{hotelId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, putHotelApiMethodUrl);
            GetHotel requestBody = new GetHotel
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Description = hotel.Description,
                IBAN = hotel.IBAN,
                Rating = hotel.Rating,
                IsActive = hotel.IsActive,
                Image = hotel.Image,
                AddressId = hotel.AddressId,
                Address = new GetAddress
                {
                    Street = hotel.Address.Street,
                    HouseNumber = hotel.Address.HouseNumber,
                    Latitude = hotel.Address.Latitude,
                    Longitude = hotel.Address.Longitude,
                    IsActive = hotel.Address.IsActive,
                    City = new GetCity
                    {
                        CityId = hotel.Address.City.CityId,
                        Name = hotel.Address.City.Name,
                        IsActive = hotel.IsActive,
                        Country = new GetCountry
                        {
                            CountryId = hotel.Address.City.Country.CountryId,
                            Name = hotel.Address.City.Country.Name,
                            IsActive = hotel.Address.City.Country.IsActive
                        }
                    }
                }
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putHotelApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task DeleteHotelAsync(string token, Guid hotelId, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var deleteHotelApiMethodUrl = $"{accommoServiceUrl}/Hotel/{hotelId}";
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, deleteHotelApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{deleteHotelApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
