using System.Text;

namespace UpdateUserByMq
{
    public class Sender
    {
        private readonly HttpClient _httpClient;
        private readonly string json;
        private readonly string Id;

        public Sender(IHttpClientFactory httpClientFactory, string userId, string json)
        {
            _httpClient = httpClientFactory.CreateClient();
            this.json = json;
            Id = userId;
        }

        public async Task SendMessage()
        {
            var putUserApiMethodUrl = $"http://booking.api:8080/UpdateUser/{Id}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Put, putUserApiMethodUrl);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Failed");
            }
        }
    }
}
