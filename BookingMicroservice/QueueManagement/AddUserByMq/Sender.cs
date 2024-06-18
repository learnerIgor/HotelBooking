using System.Text;

namespace AddUserByMq
{
    public class Sender
    {
        private readonly HttpClient _httpClient;
        private readonly string json;

        public Sender(IHttpClientFactory httpClientFactory, string json)
        {
            _httpClient = httpClientFactory.CreateClient();
            this.json = json;
        }

        public async Task SendMessage()
        {
            var postUserApiMethodUrl = $"http://booking.api:8080/User";
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, postUserApiMethodUrl);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Failed");
            }
        }
    }
}
