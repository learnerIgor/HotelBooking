namespace Users.Application.Abstractions
{
    public interface IMqService
    {
        void SendUserMessage(string exchange, string message);
        void SendMessageToExchange(string exchange, string message);
    }
}
