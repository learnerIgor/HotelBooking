using System.Text;

namespace UpdateUserPasswordByMq
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
            var putUserApiMethodUrl = $"http://localhost:5255/UpdatePassword/{Id}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Patch, putUserApiMethodUrl);
            httpRequest.Content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseMessage = await _httpClient.SendAsync(httpRequest);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Failed");
            }
        }
    }
}
