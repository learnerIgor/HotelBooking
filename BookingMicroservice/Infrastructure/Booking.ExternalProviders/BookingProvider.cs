using Booking.Application.Abstractions.ExternalProviders;
using Booking.Domain;
using Booking.ExternalProviders.Exceptions;
using Booking.ExternalProviders.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Booking.ExternalProviders
{
    public class BookingProvider : IBookingProvider
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        public BookingProvider(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient();
            _configuration = configuration;
        }
        public async Task AddBookingAsync(string token, Reservation reservation, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var postReservationApiMethodUrl = $"{accommoServiceUrl}/Bookings";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postReservationApiMethodUrl);
            BookingDto booking = new()
            {
                ReservationId = reservation.ReservationId,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                IsActive = reservation.IsActive,
                ApplicationUserId = reservation.ApplicationUserId,
                RoomId = reservation.RoomId,
            };
            var jsonBody = JsonConvert.SerializeObject(booking);

            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{postReservationApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task DeleteBookingAsync(string token, Guid reservationId, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var deleteBookingApiMethodUrl = $"{accommoServiceUrl}/Bookings/{reservationId}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, deleteBookingApiMethodUrl);
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{deleteBookingApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }

        public async Task UpdateBookingAsync(string token, string reservationId, Reservation reservation, CancellationToken cancellationToken)
        {
            var accommoServiceUrl = _configuration["AccommoServiceApiUrl"];
            var putBookingApiMethodUrl = $"{accommoServiceUrl}/Bookings/{reservationId}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, putBookingApiMethodUrl);
            BookingDto requestBody = new BookingDto
            {
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate
            };
            var jsonBody = JsonConvert.SerializeObject(requestBody);
            httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", token);
            var responseMessage = await _httpClient.SendAsync(httpRequest, cancellationToken);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var serviceName = "AccomoService";
                var requestUrlMessage = $"request url '{putBookingApiMethodUrl}'";
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                {
                    throw new ExternalServiceNotAvailable(serviceName, requestUrlMessage);
                }

                throw new ExternalServiceBadResult(serviceName, requestUrlMessage);
            }
        }
    }
}
