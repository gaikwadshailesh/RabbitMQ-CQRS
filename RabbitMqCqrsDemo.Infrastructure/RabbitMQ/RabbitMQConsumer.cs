using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqCqrsDemo.Application.Commands;
using RabbitMqCqrsDemo.Application.Handlers;

namespace RabbitMqCqrsDemo.Infrastructure.RabbitMQ;

public class RabbitMQConsumer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ProcessMessageCommandHandler _commandHandler;

    public RabbitMQConsumer(ProcessMessageCommandHandler commandHandler, string hostName = "localhost")
    {
        _commandHandler = commandHandler;
        
        var factory = new ConnectionFactory { HostName = hostName };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        
        _channel.QueueDeclare("message_queue", durable: true, exclusive: false, autoDelete: false);
    }

    public void StartConsuming()
    {
        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var command = new ProcessMessageCommand { Content = message };
            await _commandHandler.HandleAsync(command);

            _channel.BasicAck(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: "message_queue",
            autoAck: false,
            consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
} 