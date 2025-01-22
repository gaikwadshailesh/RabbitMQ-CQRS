using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqCqrsDemo.Application.Commands;
using RabbitMqCqrsDemo.Application.Handlers;
using Microsoft.Extensions.Logging;

namespace RabbitMqCqrsDemo.Infrastructure.RabbitMQ;

public class RabbitMQConsumer : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly ProcessMessageCommandHandler _commandHandler;
    private readonly ILogger<RabbitMQConsumer> _logger;

    public RabbitMQConsumer(ProcessMessageCommandHandler commandHandler, ILogger<RabbitMQConsumer> logger, string hostName = "localhost")
    {
        _commandHandler = commandHandler;
        _logger = logger;
        
        try
        {
            var factory = new ConnectionFactory { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            
            _channel.QueueDeclare("message_queue", durable: true, exclusive: false, autoDelete: false);
            _logger.LogInformation("RabbitMQ connection established successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to establish RabbitMQ connection");
            throw;
        }
    }

    public void StartConsuming()
    {
        var consumer = new EventingBasicConsumer(_channel);
        
        consumer.Received += async (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                _logger.LogInformation("Received message: {Message}", message);

                var command = new ProcessMessageCommand { Content = message };
                await _commandHandler.HandleAsync(command);

                _channel.BasicAck(ea.DeliveryTag, false);
                _logger.LogInformation("Message processed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };

        _channel.BasicConsume(queue: "message_queue",
            autoAck: false,
            consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
        _logger.LogInformation("RabbitMQ connection closed");
    }
} 