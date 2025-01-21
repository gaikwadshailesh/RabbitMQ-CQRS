using RabbitMqCqrsDemo.Application.Commands;
using RabbitMqCqrsDemo.Application.Interfaces;
using RabbitMqCqrsDemo.Domain.Entities;

namespace RabbitMqCqrsDemo.Application.Handlers;

public class ProcessMessageCommandHandler
{
    private readonly IMessageRepository _messageRepository;

    public ProcessMessageCommandHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task HandleAsync(ProcessMessageCommand command)
    {
        var message = new Message
        {
            Id = Guid.NewGuid(),
            Content = command.Content,
            ProcessedAt = DateTime.UtcNow,
            Status = "Processed"
        };

        await _messageRepository.SaveAsync(message);
    }
} 