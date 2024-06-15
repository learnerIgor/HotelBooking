using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Text;
using Booking.Application.Abstractions;
using MediatR;

namespace Booking.Exchanger
{
    public class MqEmailService : IMqEmailService
    {
        private readonly ILogger<MqEmailService> _logger;
        public MqEmailService(ILogger<MqEmailService> logger)
        {
            _logger = logger;
        }

        public Task SendEmailMessage(string queue, string message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queue,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                     routingKey: queue,
                     basicProperties: null,
                     body: body);

            _logger.LogInformation($" [x] Sent {message}");
            return Task.CompletedTask;
        }
    }
}
