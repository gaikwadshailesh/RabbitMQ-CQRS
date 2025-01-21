using RabbitMqCqrsDemo.Domain.Entities;

namespace RabbitMqCqrsDemo.Application.Interfaces;

public interface IMessageRepository
{
    Task<Message> GetByIdAsync(Guid id);
    Task SaveAsync(Message message);
} 