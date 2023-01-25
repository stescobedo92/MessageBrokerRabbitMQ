using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormulaAirLine.API.Services;

public class MessageProducer : IMessageProducer
{
    private const string HOST_NAME = "localhost";
    private const string USER_NAME = "user";
    private const string PASSWORD = "password";
    private const string VIRTUAL_HOST = "/";

    public void SendingMessage<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = HOST_NAME,
            UserName = USER_NAME,
            Password = PASSWORD,
            VirtualHost = VIRTUAL_HOST
        };

        var connnect = factory.CreateConnection();

        using var channel = connnect.CreateModel();

        channel.QueueDeclare("bookings", durable: true, exclusive: true);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("","bookings", body: body);
    }
}