using RabbitMqCqrsDemo.Application.Interfaces;
using RabbitMqCqrsDemo.Application.Queries;
using RabbitMqCqrsDemo.Domain.Entities;

namespace RabbitMqCqrsDemo.Application.Handlers;

public class GetMessageQueryHandler
{
    private readonly IMessageRepository _messageRepository;

    public GetMessageQueryHandler(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<Message> HandleAsync(GetMessageQuery query)
    {
        return await _messageRepository.GetByIdAsync(query.MessageId);
    }
} 