using RabbitMqCqrsDemo.Application.Interfaces;
using RabbitMqCqrsDemo.Domain.Entities;

namespace RabbitMqCqrsDemo.Infrastructure.Repositories;

public class InMemoryMessageRepository : IMessageRepository
{
    private readonly Dictionary<Guid, Message> _messages = new();

    public Task<Message> GetByIdAsync(Guid id)
    {
        return Task.FromResult(_messages.GetValueOrDefault(id));
    }

    public Task SaveAsync(Message message)
    {
        _messages[message.Id] = message;
        return Task.CompletedTask;
    }
} 