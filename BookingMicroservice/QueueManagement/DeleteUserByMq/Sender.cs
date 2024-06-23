namespace DeleteUserByMq
{
    public class Sender
    {
        private readonly HttpClient _httpClient;
        private readonly string id;

        public Sender(IHttpClientFactory httpClientFactory, string idUser)
        {
            _httpClient = httpClientFactory.CreateClient();
            id = idUser;
        }

        public async Task SendMessage()
        {
            var postUserApiMethodUrl = $"http://booking.api:8080/DeleteUsers/{id}";
            var httpRequest = new HttpRequestMessage(HttpMethod.Delete, postUserApiMethodUrl);
            var responseMessage = await _httpClient.SendAsync(httpRequest);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Failed");
            }
        }
    }
}
