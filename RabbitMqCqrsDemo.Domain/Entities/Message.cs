namespace RabbitMqCqrsDemo.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime ProcessedAt { get; set; }
    public string Status { get; set; }
} 