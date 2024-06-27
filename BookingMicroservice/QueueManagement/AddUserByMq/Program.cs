using AddUserByMq;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

var factory = new ConnectionFactory
{
    HostName = "rabbit",
    UserName = "guest",
    Password = "guest",
};

Thread.Sleep(60000);
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(queue: "addUser",
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var userJson = Encoding.UTF8.GetString(body);
    Sender sender = new(httpClientFactory, userJson);
    await sender.SendMessage();
    Console.WriteLine($" [x] Received {userJson}");
};
channel.BasicConsume(queue: "addUser",
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


