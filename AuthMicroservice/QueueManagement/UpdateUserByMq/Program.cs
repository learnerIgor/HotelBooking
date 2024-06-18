using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using UpdateUserByMq;
using UpdateUserByMq.Dtos;


var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();

var factory = new ConnectionFactory
{
    HostName = "rabbit",
    UserName = "guest",
    Password = "guest",
};
Thread.Sleep(30000);
using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "updateUser", type: ExchangeType.Fanout);

var quoueName = "UpdateUserAuth";
channel.QueueDeclare(queue: quoueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

channel.QueueBind(queue: quoueName,
    exchange: "updateUser",
    routingKey: string.Empty);

Console.WriteLine(" [*] Waiting for messages.");

var consumer = new EventingBasicConsumer(channel);
consumer.Received += async (model, ea) =>
{
    var body = ea.Body.ToArray();
    var userJson = Encoding.UTF8.GetString(body);
    var user = JsonSerializer.Deserialize<UserDto>(userJson);
    var userLogin = new { user!.Login };
    Sender sender = new(httpClientFactory, user.Id, JsonSerializer.Serialize(userLogin));
    await sender.SendMessage();
    Console.WriteLine($" [x] Received {userJson}");
};
channel.BasicConsume(queue: quoueName,
                     autoAck: true,
                     consumer: consumer);

Console.WriteLine(" Press [enter] to exit.");
Console.ReadLine();


