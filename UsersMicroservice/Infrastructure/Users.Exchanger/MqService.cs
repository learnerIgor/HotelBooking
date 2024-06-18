using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using Users.Application.Abstractions;

namespace Users.Exchanger
{
    public class MqService : IMqService
    {
        private readonly ILogger<MqService> _logger;
        public MqService(ILogger<MqService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Exchanger has several queues
        /// </summary>
        public void SendMessageToExchange(string exchange, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbit",
                UserName = "guest",
                Password = "guest",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Fanout);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: exchange,
                     routingKey: string.Empty,
                     basicProperties: null,
                     body: body);

            _logger.LogInformation($" [x] Sent {message}");
        }

        /// <summary>
        /// Exchanger has only one queue
        /// </summary>
        public void SendUserMessage(string exchange, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "rabbit",
                UserName = "guest",
                Password = "guest",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: exchange,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                     routingKey: exchange,
                     basicProperties: null,
                     body: body);

            _logger.LogInformation($" [x] Sent {message}");
        }
    }
}
