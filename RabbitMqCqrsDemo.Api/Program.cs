using RabbitMqCqrsDemo.Application.Handlers;
using RabbitMqCqrsDemo.Application.Interfaces;
using RabbitMqCqrsDemo.Infrastructure.RabbitMQ;
using RabbitMqCqrsDemo.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services
builder.Services.AddSingleton<IMessageRepository, InMemoryMessageRepository>();
builder.Services.AddScoped<ProcessMessageCommandHandler>();
builder.Services.AddScoped<GetMessageQueryHandler>();
builder.Services.AddSingleton<RabbitMQConsumer>();

var app = builder.Build();

// Configure RabbitMQ Consumer
var consumer = app.Services.GetRequiredService<RabbitMQConsumer>();
consumer.StartConsuming();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run(); 